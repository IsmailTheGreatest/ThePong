using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { PaddleSizeIncrease, PaddleSpeedIncrease }
    public PowerUpType powerUpType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if Player A or Player B has collided with the power-up
        if (other.CompareTag("PlayerA") || other.CompareTag("PlayerB"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Apply the power-up based on its type
                if (powerUpType == PowerUpType.PaddleSizeIncrease)
                {
                    player.IncreasePaddleSize();
                    
                }
                else if (powerUpType == PowerUpType.PaddleSpeedIncrease)
                {
                    player.IncreasePaddleSpeed();
                }

                // Destroy the power-up after it's collected
                Destroy(gameObject);
            }
        }
    }
}
