using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RollerCoasterTrain : MonoBehaviour
{
    [Tooltip("AudioSource that contains the minecart sound. If left empty, will use the AudioSource on this GameObject.")]
    public AudioSource cartAudio;

    [Tooltip("Minimum speed (units/s) to consider the cart 'moving'.")]
    public float speedThreshold = 0.05f;

    Rigidbody rb;
    Vector3 prevPos;

    void Start()
    {
        if (cartAudio == null)
            cartAudio = GetComponent<AudioSource>();

        if (cartAudio != null)
        {
            cartAudio.loop = true;
            // don't play automatically; we'll control playback in code
            cartAudio.playOnAwake = false;
        }

        rb = GetComponent<Rigidbody>();
        prevPos = transform.position;
    }

    void Update()
    {
        if (cartAudio == null)
            return;

        float speed = 0f;

        if (rb != null)
        {
            speed = rb.linearVelocity.magnitude;
        }
        else
        {
            // fallback: estimate speed from position delta
            speed = (transform.position - prevPos).magnitude / Mathf.Max(Time.deltaTime, 1e-6f);
            prevPos = transform.position;
        }

        if (speed > speedThreshold)
        {
            if (!cartAudio.isPlaying)
                cartAudio.Play();
        }
        else
        {
            if (cartAudio.isPlaying)
                cartAudio.Pause();
        }
    }
}