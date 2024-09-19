using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // The prefab for the power-up
    public float spawnInterval = 10f; // Time interval between spawns
    public float powerUpDuration = 5f; // Duration for which the power-up lasts

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Randomly choose between x = 7 or x = -7
            float xPos = (Random.value > 0.5f) ? 7f : -7f; 
            
            // Random y position between -7.5 and 7.5
            float yPos = Random.Range(-7.5f, 7.5f);

            Vector2 spawnPosition = new Vector2(xPos, yPos);

            // Instantiate the power-up
            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Randomly assign the power-up type (either size increase or speed increase)
            PowerUp powerUpComponent = spawnedPowerUp.GetComponent<PowerUp>();
            powerUpComponent.powerUpType = (Random.value > 0.5f) ? 
                PowerUp.PowerUpType.PaddleSizeIncrease : 
                PowerUp.PowerUpType.PaddleSpeedIncrease;

            // Destroy the power-up after the specified duration if not collected
            Destroy(spawnedPowerUp, powerUpDuration);
        }
    }
}
