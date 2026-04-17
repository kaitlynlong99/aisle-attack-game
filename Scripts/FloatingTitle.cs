using TMPro.Examples;
using UnityEngine;

public class FloatingTitle : MonoBehaviour
{
    private Vector3 startPos;

    public float floatAmount = 10f; // Amount to float up and down
    public float floadSpeed = 2f; // Speed of the floating effect

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floadSpeed) * floatAmount; // Calculate the new Y position using a sine wave for smooth floating effect
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z); // Update the local position of the title to create the floating effect
    }
}
