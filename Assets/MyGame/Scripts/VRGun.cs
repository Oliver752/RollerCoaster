using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VRGun : MonoBehaviour
{
    public Transform gunSnapTransform;
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    public ParticleSystem gunFireParticleSystem;
    public AudioSource gunFireAudioSource;
    public Collider[] gunColliders;

    public float fireSpeed = 125.0f;

    private Vector3 gunVelocity;
    private Vector3 previousPosition;
    private bool fireFlag = false;
    
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        // Velocity is distance between current position and previous position divided by time.
        gunVelocity = (bulletSpawnTransform.position - previousPosition) / Time.deltaTime;
 
        // Store the position at the end of the Update() function for the next frame.
        previousPosition = bulletSpawnTransform.position;
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        // If the fireFlag boolean was set to true during Update()
        // call the Fire() function and set the fireFlag back to false.
        if(fireFlag == true)
        {
            fireFlag = false;
            Fire();
        }
    }

    // Called by Gun.XRGrabInteractable.InteractableEvents.Activated, when the gun is fired.
    public void FireBullet()
    {
        // Set the boolean fireFlag variable to true so that in the next
        // FixedUpdate loop iteration the Fire() function can be called. 
        fireFlag = true;
    }

    // Called by VRGun.XRGrabInteractable.InteractableEvents.Activated, when the gun is fired.
    public void Fire()
    {
        Debug.Log("Gun is Fired!!!");

        // Play the gunFireParticleSystem and gunFireAudioSource.
        gunFireParticleSystem.Play();
        gunFireAudioSource.Play();

        // Spawn/Instantiate a clone of the Bullet-Prefab and store
        // a reference to it in the GameObject spawnedBullet variable.
        GameObject spawnedBullet = Instantiate(bulletPrefab);

        // Position the spawnedBullet at the tip of the gun at the bulletSpawnTransform.position.
        spawnedBullet.transform.position = bulletSpawnTransform.transform.position;
        // Rotate the spawnedBullet at the tip of the gun at the bulletSpawnTransform.rotation.
        spawnedBullet.transform.rotation = bulletSpawnTransform.transform.rotation;

        // Get the spawnedBullet's Rigidbody component and set its velocity
        // to the forward direction of the bullet multiplied by the fireSpeed.
        spawnedBullet.GetComponent<Rigidbody>().linearVelocity = gunVelocity + (spawnedBullet.transform.forward * fireSpeed);

        SphereCollider spawnedBulletCollider = spawnedBullet.GetComponent<SphereCollider>();
        for(int i = 0; i < gunColliders.Length; i++)
        {
            Physics.IgnoreCollision(spawnedBulletCollider, gunColliders[i], true);
        }

        Destroy(spawnedBullet, 5.0f);
    }

    public void OnGrab()
    {
        Debug.Log("Gun is Grabbed!!!");
    }

    public void OnRelease()
    {
        Debug.Log("Gun is Dropped!!!");

        transform.position = gunSnapTransform.position;
        transform.rotation = gunSnapTransform.rotation;

        GetComponent<Rigidbody>().isKinematic = true;
    }
}