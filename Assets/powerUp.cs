using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { PaddleSizeIncrease, PaddleSpeedIncrease }
    public PowerUpType powerUpType;

    // Sound effect for picking up the power-up
    public AudioClip pickupSound; 

    private AudioSource audioSource;

    private void Start()
    {
        // Ensure there is an AudioSource component attached to this power-up
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

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

                // Play pickup sound
                PlayPickupSound();

                // Destroy the power-up after it's collected (after the sound is played)
                Destroy(gameObject, pickupSound.length);
            }
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
