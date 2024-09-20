using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider powerUpSlider; // Slider to show power-up duration
    public float speed = 10f;
    public bool isPlayerA = false; // Set this in the Inspector or by script
    private Rigidbody2D rb;
    private Vector2 playerMovement;

    private Vector3 originalScale;
    private float originalSpeed;
    public PowerUpSpawner spawner;
    private float powerUpTimeRemaining; // Time remaining for active power-up
    public GameObject ball;
    public GameManager gameManager;

    private bool hasActivePowerUp = false; // Flag for active power-up

    // Paddle color change
    private Color originalColor; // Store the original paddle color
    public Color powerUpColor = Color.red; // Color when the power-up is active
    private Material paddleMaterial; // Reference to the paddle's material

    void Start()
    {
        powerUpTimeRemaining = 0f;
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Capture the original scale at the start
        originalSpeed = speed;

        // Get the material component and store the original color
        paddleMaterial = GetComponent<Renderer>().material;
        originalColor = paddleMaterial.color; // Capture the original paddle color

        // Ensure the power-up slider is hidden at the start
        powerUpSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameManager.isTwoPlayerMode)
        {
            if (isPlayerA)
            {
                HandlePlayerAInput(); // Control for Player A using W and S keys
            }
            else
            {
                HandlePlayerBInput(); // Control for Player B using arrow keys
            }
        }
        else
        {
            if (isPlayerA)
            {
                PaddleAController();
            }
            else
            {
                PaddleBController();
            }
        }

        // Handle the power-up timer and slider
        if (hasActivePowerUp)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            powerUpSlider.value = powerUpTimeRemaining;

            // If the power-up time has expired, reset the effects
            if (powerUpTimeRemaining <= 0)
            {
                ResetPowerUpEffects();
            }
        }
    }

    // Control the AI for Paddle B (autonomous movement)
    private void PaddleBController()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMovement = new Vector2(0, 1);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMovement = new Vector2(0, -1);
        }
        else
        {
            playerMovement = new Vector2(0, 0);
        }
    }

    // Control for Paddle A when playing against AI
    private void PaddleAController()
    {
        playerMovement = new Vector2(0, Input.GetAxis("Vertical"));
    }

    // Control for Player A (W and S keys)
    private void HandlePlayerAInput()
    {
        if (Input.GetKey(KeyCode.W)) // Move up
        {
            playerMovement = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S)) // Move down
        {
            playerMovement = Vector2.down;
        }
        else
        {
            playerMovement = Vector2.zero;
        }
    }

    // Control for Player B (Up and Down arrow keys)
    private void HandlePlayerBInput()
    {
        if (Input.GetKey(KeyCode.UpArrow)) // Move up
        {
            playerMovement = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // Move down
        {
            playerMovement = Vector2.down;
        }
        else
        {
            playerMovement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMovement * speed;
    }

    // Power-up effect for paddle size increase
    public void IncreasePaddleSize()
    {
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 2, originalScale.z); // Double the paddle size
        ActivatePowerUp(spawner.powerUpDuration);
    }

    // Power-up effect for speed increase
    public void IncreasePaddleSpeed()
    {
        speed *= 2f; // Double the paddle speed
        paddleMaterial.color = powerUpColor; // Change the paddle color during the power-up
        ActivatePowerUp(spawner.powerUpDuration);
    }

    // Method to handle power-up activation, set duration, and show the slider
    private void ActivatePowerUp(float duration)
    {
        hasActivePowerUp = true;
        powerUpTimeRemaining = duration;

        // Activate and set the slider values
        powerUpSlider.gameObject.SetActive(true);
        powerUpSlider.maxValue = duration;
        powerUpSlider.value = duration;

        // Call ResetPowerUpEffects after duration
        Invoke(nameof(ResetPowerUpEffects), duration);
    }

    // Reset the paddle to its original size, speed, and color after the power-up expires
    private void ResetPowerUpEffects()
    {
        hasActivePowerUp = false;

        // Reset size, speed, and color
        ResetSize();
        ResetSpeed();
        paddleMaterial.color = originalColor; // Reset to the original paddle color

        // Hide the power-up slider
        powerUpSlider.gameObject.SetActive(false);
    }

    public void ResetSpeed()
    {
        speed = originalSpeed;
    }

    public void ResetSize()
    {
        transform.localScale = originalScale;
    }
}
