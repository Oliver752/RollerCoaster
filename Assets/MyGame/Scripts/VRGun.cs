using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VRGun : MonoBehaviour
{
    public Transform gunSnapTransform;
    
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
         
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