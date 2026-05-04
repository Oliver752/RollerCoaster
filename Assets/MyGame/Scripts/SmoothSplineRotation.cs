using UnityEngine;

public class SmoothSplineRotation : MonoBehaviour
{
    public Transform splineTarget; // drag the spline-animated object here
    public float positionFollowSpeed = 999f; // instant position
    public float rotationSmoothSpeed = 3f;   // tweak this (lower = smoother)

    void Update()
    {
        // Position follows instantly
        transform.position = splineTarget.position;

        // Rotation lerps smoothly
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            splineTarget.rotation,
            Time.deltaTime * rotationSmoothSpeed
        );
    }
}