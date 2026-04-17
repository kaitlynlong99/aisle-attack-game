using UnityEngine;

public class RandomChildSprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int childCount = transform.childCount; // Get the number of child objects under this GameObject

        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (childCount > 0)
        {   
            int randomIndex = Random.Range(0, childCount); // Generate a random index to select one of the child objects
            transform.GetChild(randomIndex).gameObject.SetActive(true); // Activate the randomly selected child object
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
