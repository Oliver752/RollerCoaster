using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.Splines;
using Unity.Mathematics;

public class CabinCameraAlign : MonoBehaviour
{
    [SerializeField] private Transform trainTransform;
    [SerializeField] private Transform vrPlayer;
    [SerializeField] private bool autoFindReferences = true;
    [SerializeField] private bool followTrainMotionDirection = true;
    [SerializeField] private bool followSplineDirection = true;
    [SerializeField] private bool followPitchAndRoll = false;
    [SerializeField] private float rotationSmooth = 12f;

    private float currentYaw;
    private SplineAnimate splineAnimate;
    private SplineContainer splineContainer;
    private XROrigin xrOrigin;
    private Vector3 lastTrainPosition;
    private bool hasLastTrainPosition;

    void Start()
    {
        if (autoFindReferences)
        {
            if (vrPlayer == null)
                vrPlayer = GetComponentInChildren<XROrigin>()?.transform;

            if (vrPlayer == null)
                vrPlayer = transform.Find("VRPlayer");

            if (trainTransform == null)
            {
                if (transform.parent != null)
                    trainTransform = transform.parent;
                else
                    trainTransform = transform;
            }

            if (splineAnimate == null)
                splineAnimate = trainTransform.GetComponent<SplineAnimate>();

            if (splineContainer == null && splineAnimate != null)
                splineContainer = splineAnimate.Container;
        }

        if (vrPlayer != null)
            xrOrigin = vrPlayer.GetComponent<XROrigin>() ?? vrPlayer.GetComponentInChildren<XROrigin>();

        if (vrPlayer != null)
            currentYaw = vrPlayer.eulerAngles.y;

        if (trainTransform != null)
        {
            lastTrainPosition = trainTransform.position;
            hasLastTrainPosition = true;
        }
    }

    void LateUpdate()
    {
        if (vrPlayer == null || trainTransform == null)
            return;

        if (splineAnimate == null)
            splineAnimate = trainTransform.GetComponent<SplineAnimate>();

        if (splineContainer == null && splineAnimate != null)
            splineContainer = splineAnimate.Container;

        if (followPitchAndRoll)
        {
            vrPlayer.rotation = Quaternion.Slerp(
                vrPlayer.rotation,
                trainTransform.rotation,
                1f - Mathf.Exp(-rotationSmooth * Time.deltaTime));
            return;
        }

        var forward = trainTransform.forward;

        if (followTrainMotionDirection)
        {
            if (!hasLastTrainPosition)
            {
                lastTrainPosition = trainTransform.position;
                hasLastTrainPosition = true;
            }

            var motion = trainTransform.position - lastTrainPosition;
            lastTrainPosition = trainTransform.position;

            if (motion.sqrMagnitude > 0.000001f)
                forward = motion.normalized;
        }
        else if (followSplineDirection && splineAnimate != null && splineContainer != null && splineContainer.Spline != null)
        {
            var t = Mathf.Repeat(splineAnimate.NormalizedTime, 1f);
            var tangentLocal = SplineUtility.EvaluateTangent(splineContainer.Spline, t);
            if (math.lengthsq(tangentLocal) > 0.0001f)
                forward = splineContainer.transform.TransformDirection(tangentLocal).normalized;
        }

        var flatForward = Vector3.ProjectOnPlane(forward, Vector3.up);
        if (flatForward.sqrMagnitude < 0.0001f)
            return;

        var targetYaw = Quaternion.LookRotation(flatForward, Vector3.up).eulerAngles.y;
        var sourceYaw = xrOrigin != null ? xrOrigin.transform.eulerAngles.y : vrPlayer.eulerAngles.y;
        currentYaw = Mathf.LerpAngle(sourceYaw, targetYaw, 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime));

        var deltaYaw = Mathf.DeltaAngle(sourceYaw, currentYaw);
        if (Mathf.Abs(deltaYaw) < 0.001f)
            return;

        if (xrOrigin != null)
            xrOrigin.RotateAroundCameraUsingOriginUp(deltaYaw);
        else
            vrPlayer.rotation = Quaternion.Euler(0f, currentYaw, 0f);
    }
}