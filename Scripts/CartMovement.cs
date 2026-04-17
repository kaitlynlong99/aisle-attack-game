using UnityEngine;

public class CartMovement : MonoBehaviour
{
    public float speed = 8f; // Speed of the cart movement

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)

        float movement = horizontalInput * speed * Time.deltaTime; // Calculate movement based on input and speed
        transform.Translate(movement, 0, 0); // Move the cart horizontally
    }
}
