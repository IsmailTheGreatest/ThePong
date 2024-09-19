using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public BallController ballController; // Reference to the BallController
    public  bool isTwoPlayerMode = false;
    private void Start()
    {
        Time.timeScale = 0f; // Pause the game initially
        menuCanvas.enabled = true; // Show the menu
    }

    // Start the game against the computer
    public void StartVsComputer()
    {
        isTwoPlayerMode = false;
        StartGame();
    }

    // Start the game in two-player mode
    public void StartTwoPlayer()
    {
       isTwoPlayerMode = true;
        StartGame();
    }

    // Common method to start the game
    private void StartGame()
    {
        menuCanvas.enabled = false; // Hide the menu
        Time.timeScale = 1f; // Resume the game
        ballController.StartGame(); // Tell the ball to start moving
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
