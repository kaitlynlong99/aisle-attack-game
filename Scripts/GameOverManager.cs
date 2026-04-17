using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Retrieve the final score from PlayerPrefs, defaulting to 0 if it doesn't exist
        finalScoreText.text = "Final Score: " + finalScore; // Update the final score text to display the player's final score
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay"); // Reload the current scene to restart the game
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); // Load the main menu scene
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
