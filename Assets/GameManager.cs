using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas; // Main menu canvas
    public GameObject instructionsPanel; // Panel for instructions
    public BallController ballController; // Reference to the BallController
    public Button helpButton; // Help button to display instructions
    public bool isTwoPlayerMode = false;

    private void Start()
    {
        Time.timeScale = 0f; // Pause the game initially
        menuCanvas.enabled = true; // Show the menu
        instructionsPanel.SetActive(false); // Ensure instructions are hidden initially

        // Add listener to Help button
        helpButton.onClick.AddListener(ToggleInstructions);
    }

    // Method to start the game against the computer
    public void StartVsComputer()
    {
        isTwoPlayerMode = false;
        StartGame();
    }

    // Method to start the game in two-player mode
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

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Method to toggle instructions panel visibility
    public void ToggleInstructions()
    {
        bool isActive = instructionsPanel.activeSelf;
        instructionsPanel.SetActive(!isActive); // Toggle instructions panel
    }
    public void CloseInstructions()
{
    instructionsPanel.SetActive(false); // Hide the instructions panel
    
}

}
