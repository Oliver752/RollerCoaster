using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;
 
public class GameManager : MonoBehaviour
{
    public VRPlayer player;
    public RollerCoasterTrain rollerCoasterTrain;
    public GameObject tutorialCanvas;
    public GameOverCanvas gameOverCanvas;
    public PlayerScoreCanvas playerScoreCanvas;
 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The Game is Starting!!!");

        gameOverCanvas.gameObject.SetActive(false);

        player.playerScore = 0;
        playerScoreCanvas.playerScoreValueText.text = "0";
    }
 
    // Update is called once per frame
    void Update()
    {
        Debug.Log("The Game is Running!!!");

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreasePlayerScore(100);
        }
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

    public void GameOver()
    {
        Debug.Log("Game Over!!!");

        gameOverCanvas.playerScoreValueText.text = "" + player.playerScore;

        gameOverCanvas.gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
 
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreasePlayerScore(int amount)
    {
        Debug.Log("Adding " + amount + " points to Player Score!!!");
 
        // Increase the playerScore value on the player
        player.playerScore += amount;

        // Update the score displayed on the PlayerScoreCanvas
        playerScoreCanvas.playerScoreValueText.text = "" + player.playerScore;
    }
}