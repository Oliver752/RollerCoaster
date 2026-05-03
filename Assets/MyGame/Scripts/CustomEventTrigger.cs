using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class CustomEventTrigger : MonoBehaviour
{

    public string triggerTag ="Player";

    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerStayEvent;
    public UnityEvent onTriggerExitEvent;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,1);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
         
    }

    public IEnumerator OnTriggerEnter(Collider collider)
    {
        Debug.Log("A GameObject named " + collider.gameObject.name + " with the tag " + collider.gameObject.tag + " has Entered the trigger!!!");

        if(onTriggerEnterEvent != null && collider.gameObject.tag == triggerTag)
        {
            onTriggerEnterEvent.Invoke();
        }

        yield return null;
    }

    public IEnumerator OnTriggerStay(Collider collider)
    {
        if(onTriggerStayEvent != null && collider.gameObject.tag == triggerTag)
        {
             onTriggerStayEvent.Invoke();
        }
        yield return null;
    }

    public IEnumerator OnTriggerExit(Collider collider)
    {
        if(onTriggerExitEvent != null && collider.gameObject.tag == triggerTag)
        {
            onTriggerExitEvent.Invoke();
        }
        yield return null;
    }
}