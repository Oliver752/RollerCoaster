using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
 
public class GameManager : MonoBehaviour
{
    public RollerCoasterTrain rollerCoasterTrain;
    public GameObject tutorialCanvas;
 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The Game is Starting!!!");
    }
 
    // Update is called once per frame
    void Update()
    {
        Debug.Log("The Game is Running!!!");
    }
 
    public void StartRollerCoasterRide()
    {
        Debug.Log("The Ride is Starting!!!");
 
        // Play the spline animation to start the ride.
        rollerCoasterTrain.GetComponent<SplineAnimate>().Play();
 
        // Deactivate the tutorial canvas when the player starts the ride.
        tutorialCanvas.SetActive(false);
    }

    public void StopRollerCoasterRide()
    {
        Debug.Log("The Ride is Stopping!!!");

        rollerCoasterTrain.GetComponent<SplineAnimate>().Pause();
    }
}