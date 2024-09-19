using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = false; // Set this in the Inspector or by script
    private Rigidbody2D rb;
    private Vector2 playerMovement;
    
    private Vector3 originalScale;
    private float originalSpeed;
    public PowerUpSpawner spawner;
    private float duration;
    public GameObject circle;
public GameManager gameManager;
    void Start()
    {
        duration = spawner.powerUpDuration;
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Capture the original scale at the start
        originalSpeed = speed;
    }

    void Update()
    { if(gameManager.isTwoPlayerMode){
        if (isPlayerA)
        {
            HandlePlayerAInput(); // Control for Player A using W and S keys
        }
        else
        {
            HandlePlayerBInput(); // Control for Player B using arrow keys
        }}
        else {
                   if(isPlayerA){
            PaddleAController();
        }
        else{
            PaddleBController();
        } 
        }
    }


     private void PaddleBController(){
        if(circle.transform.position.y > transform.position.y + 0.5f){
            playerMovement = new Vector2(0, 1);
        }else if(circle.transform.position.y < transform.position.y - 0.5f){
            playerMovement = new Vector2(0, -1);
        }else {
            playerMovement = new Vector2(0, 0);
        }
    }

    private void PaddleAController(){
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
        Invoke(nameof(ResetSize), duration);
    }

    // Power-up effect for speed increase
    public void IncreasePaddleSpeed()
    {
        speed *= 2f; // Increase paddle speed
        Invoke(nameof(ResetSpeed), duration); // Reset speed after power-up duration
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
