using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void LoadGameplay()
    {         
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadInstructions()
    {         
        SceneManager.LoadScene("Instructions");
    }

    public void LoadMainMenu()
    {         
        SceneManager.LoadScene("StartMenu");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
