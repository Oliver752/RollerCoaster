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

    public float fireSpeed = 125.0f;
    
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
         
    }

    // Called by VRGun.XRGrabInteractable.InteractableEvents.Activated, when the gun is fired.
    public void FireBullet()
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
        spawnedBullet.GetComponent<Rigidbody>().linearVelocity = spawnedBullet.transform.forward * fireSpeed;
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