using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public bool isBadItem = false; // Flag to indicate if the item is a bad item (optional, can be used for different behaviors)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cart")) // Check if the colliding object has the tag "Cart"
        {
            if (isBadItem)
            {
                GameManager gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene
                gameManager.LoseLife(); // Call the LoseLife method to decrease the player's lives
                Destroy(gameObject); // Destroy the falling item when a cart collides with it
                return; // Exit the method to prevent adding score for bad items
            }
            else 
            {
                GameManager gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene
                gameManager.AddScore(10); // Call the AddScore method to increase the player's score by 10 points
                Destroy(gameObject); // Destroy the falling item when a cart collides with it
                return;
            }
        }

        if(other.CompareTag("MissZone"))
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene
            if (!isBadItem) // Only lose a life if it's not a bad item, as bad items should not penalize the player for missing them
            {
                gameManager.LoseLife(); // Call the LoseLife method to decrease the player's lives
            }
            Destroy(gameObject); // Destroy the falling items when the cart misses it
            return;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
