using UnityEngine;
using TMPro; // Import TextMeshPro namespace for UI text handling
using UnityEngine.SceneManagement; // Import SceneManagement namespace for scene handling (e.g., restarting the game)
using UnityEngine.UI; // Import UnityEngine.UI namespace for UI components (e.g., buttons)
using System.Collections;
using UnityEditor.Purchasing; // Import System.Collections namespace for potential use of coroutines (optional, can be removed if not using coroutines)

public class GameManager : MonoBehaviour
{
    public int lives = 3; // Number of lives the player has
    public bool isGameOver = false; // Flag to indicate if the game is over
    public int score = 0; // Player's score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProGUI component for displaying the score
    public TextMeshProUGUI livesText; // Reference to the TextMeshProGUI component for displaying the lives
    public GameObject cart; // Reference to the cart GameObject for potential interactions (e.g., enabling/disabling movement)
   

    public int currentLevel = 1; // Track the current level of the game (optional, can be used for increasing difficulty or other level-based features)
    public TextMeshProUGUI levelText; // Reference to the TextMeshProGUI component for displaying the current level (optional, can be used for showing the player their current level)
    public TextMeshProUGUI gameOverText; // Reference to the TextMeshProGUI component for displaying the game over message (optional, can be used for showing a game over message when the player loses all lives)
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI youWinText;
    public TextMeshProUGUI winFinalScoreText;
    public GameObject restartButton;
    public GameObject replayButton;
    public GameObject level1Background;
    public GameObject level2Background;
    public GameObject level3Background;
    public GameObject level4Background;
    public GameObject level5Background;
    public GameObject youWinBackground;
    public GameObject scorePanel;
    public GameObject livesPanel;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite heartFull;
    public Sprite heartBroken;
    public GameObject levelPanel;
    public GameObject winPanel;
    public GameObject gameOverBackground;
    public GameObject gameOverPanel;
    ItemSpawner itemSpawner; // Reference to the ItemSpawner component for controlling item spawning (optional, can be used for adjusting spawn rates or other spawning behavior based on the game state)

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip goodCatchSound;
    public AudioClip badCatchSound;
    public AudioClip gameOverSound;
    public AudioClip levelUpSound;
    public AudioClip winSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        itemSpawner = FindFirstObjectByType<ItemSpawner>(); // Find and store a reference to the ItemSpawner component in the scene (optional, can be used for controlling item spawning based on game state or other conditions)
        currentLevel = 1;
        score = 0;
        lives = 3;

        scoreText.text = "Score:  0"; // Initialize the score text to show the starting score
        heart1.sprite = heartFull; // Set the first heart icon to full at the start of the game (optional, can be removed if not using heart icons for lives)
        heart2.sprite = heartFull; // Set the second heart icon to full at the start of the game (optional, can be removed if not using heart icons for lives)
        heart3.sprite = heartFull; // Set the third heart icon to full at the start of the game (optional, can be removed if not using heart icons for lives)
        levelText.gameObject.SetActive(false); // Hide the level text at the start of the game (optional, can be removed if you want to show the level from the beginning)
        levelPanel.SetActive(false); // Hide the level panel at the start of the game (optional, can be removed if you want to show the level panel from the beginning)

        level1Background.gameObject.SetActive(true);
        level2Background.gameObject.SetActive(false);
        level3Background.gameObject.SetActive(false);
        level4Background.gameObject.SetActive(false);
        level5Background.gameObject.SetActive(false);
        youWinBackground.gameObject.SetActive(false);

        winPanel.SetActive(false);

        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic; // Set the background music clip to the music source (optional, can be removed if not using background music)
            musicSource.loop = true; // Set the music source to loop the background music (optional, can be removed if you want the music to play only once)
            musicSource.Play(); // Play the background music at the start of the game (optional, can be removed if not using background music)
        }

        itemSpawner.StartSpawning(); // Start spawning items at the beginning of the game (optional, can be removed if you want to control when item spawning starts based on other conditions)
    }

    public void StartGame()
    {
        level1Background.SetActive(true); // Show the level 1 background when the game starts (optional, can be removed if not using different backgrounds for levels)
        
        scorePanel.SetActive(true); // Show the score panel when the game starts (optional, can be removed if not using a score panel)
        scoreText.gameObject.SetActive(true);
        livesPanel.SetActive(true); // Show the lives panel when the game starts (optional, can be removed if not using a lives panel)
        livesText.gameObject.SetActive(true);

        cart.SetActive(true);

        musicSource.clip = backgroundMusic; // Set the background music clip to the music source (optional, can be removed if not using background music)
        musicSource.Play(); // Play the background music when the game starts (optional, can be removed if not using background music)

        FindFirstObjectByType<ItemSpawner>().StartSpawning(); // Start spawning items when the game starts
    }

     void HideLevelText()
        {
            levelText.gameObject.SetActive(false); // Hide the level text after a short delay (optional, can be adjusted as needed
        }

    void HideLevelPanel()
    {   
        levelPanel.SetActive(false); // Hide the level panel after a short delay (optional, can be adjusted as needed
    }

    void LevelUp(string levelName)
    {
        itemSpawner.StopSpawning();
        itemSpawner.spawnInterval *= 0.8f; // Decrease spawn interval to increase difficulty (optional, can be adjusted as needed)
        itemSpawner.goodItemChance *= 0.6f; // Decrease the chance of spawning good items to increase difficulty (optional, can be adjusted as needed)
        itemSpawner.StartSpawning(); // Restart spawning with the new spawn interval

        if (levelUpSound != null)
        {
            sfxSource.PlayOneShot(levelUpSound); // Play the level up sound effect when the player reaches a new level (optional, can be removed if not using sound effects)
        }

        levelText.text = levelName; // Update the level text to show the new level (optional, can be removed if not using a level display)
        levelPanel.SetActive(true);
        levelText.gameObject.SetActive(true);
        Invoke("HideLevelText", 2f);
        Invoke("HideLevelPanel", 2f); // Hide the level panel after a short delay (optional, can be adjusted as needed)
    }

    IEnumerator AnimateBrokenHeart(Image heart)
    {   Vector3 originalScale = heart.transform.localScale;
        Vector3 originalPos = heart.transform.localPosition;

        heart.transform.localScale = originalScale * 1.2f;
        heart.transform.localPosition = originalPos + new Vector3(-5f, 0f, 0f);
        yield return new WaitForSeconds(0.04f);

        heart.transform.localPosition = originalPos + new Vector3(5f, 0f, 0f);
        yield return new WaitForSeconds(0.04f);

        heart.transform.localPosition = originalPos; // Reset position to original after animation
        heart.transform.localScale = originalScale; // Reset scale to original after animation

    }

    void UpdateHeartsWithAnimation()
    {
        if (lives == 2)
        {
            heart3.sprite = heartBroken; // Update the third heart icon to broken when lives decrease to 2
            StartCoroutine(AnimateBrokenHeart(heart3)); // Animate the third heart breaking when lives decrease to 2
        }
        else if (lives == 1)
        {
            heart2.sprite = heartBroken; // Update the second heart icon to broken when lives decrease to 1
            StartCoroutine(AnimateBrokenHeart(heart2)); // Animate the second heart breaking when lives decrease to 1
        }
        else if (lives == 0)
        {
            heart1.sprite = heartBroken; // Update the first heart icon to broken when lives decrease to 0
            StartCoroutine(AnimateBrokenHeart(heart1)); // Animate the first heart breaking when lives decrease to 0
        }
    }
    void GameWin()
    {
        isGameOver = true;

        musicSource.Stop(); // Stop the background music when the player wins the game (optional, can be removed if you want the music to continue during the win screen)

        if (winSound != null)
        { 
            sfxSource.PlayOneShot(winSound); // Play the win sound effect when the player wins the game (optional, can be removed if not using sound effects)
        }

        itemSpawner.StopSpawning(); // Stop spawning items when the player wins the game (optional, can be removed if you want to continue spawning items after winning)

        cart.SetActive(false); // Hide the cart when the player wins the game (optional, can be removed if you want the cart to remain visible after winning)

        scorePanel.SetActive(false); // Hide the score panel when the player wins the game (optional, can be removed if you want the score to remain visible after winning)
        scoreText.gameObject.SetActive(false);

        livesPanel.SetActive(false); // Hide the lives panel when the player wins the game (optional, can be removed if you want the lives to remain visible after winning)
        livesText.gameObject.SetActive(false);

        levelPanel.SetActive(false); // Hide the level panel when the player wins the game (optional, can be removed if you want the level to remain visible after winning)
        levelText.gameObject.SetActive(false);

        level1Background.SetActive(false); // Hide the level 1 background when the player wins the game (optional, can be removed if not using different backgrounds for levels)
        level2Background.SetActive(false);
        level3Background.SetActive(false);
        
        level4Background?.SetActive(false); 
        level5Background?.SetActive(false);
        youWinBackground.SetActive(true); // Show the "You Win" background when the player wins the game (optional, can be removed if not using a win background)

        winPanel.SetActive(true);
        youWinText.gameObject.SetActive(true); // Show the "You Win" text when the player wins the game (optional, can be removed if not using a win text)
        winFinalScoreText.gameObject.SetActive(true); // Show the final score text on the win screen when the player wins the game (optional, can be removed if not showing a final score on the win screen)
        winFinalScoreText.text = "Final Score: " + score; // Update the final score text to display the player's final score when they win (optional, can be removed if not showing a final score on the win screen)
        replayButton.SetActive(true); // Show the replay button when the player wins the game (optional, can be removed if not using a replay button on the win screen)
    }
    void CheckLevelProgression()
    {
        if (score >= 40 && currentLevel == 1)
        {
            currentLevel++; // Increase the level when the score reaches 100 (optional, can be used for increasing difficulty or other level-based features)
            level1Background.SetActive(false); // Hide the level 1 background when the player reaches level 2 (optional, can be removed if not using different backgrounds for levels)
            level2Background.SetActive(true); // Show the level 2 background when the player reaches level 2 (optional, can be removed if not using different backgrounds for levels)

            LevelUp("LEVEL 2"); // Call the LevelUp method to handle level progression and display the new level (optional, can be removed if not using levels or a level display)
        }



        if (score >= 80 && currentLevel == 2)
        {
            currentLevel++; // Increase the level when the score reaches 200 (optional, can be used for increasing difficulty or other level-based features)
            level2Background.SetActive(false); // Hide the level 2 background when the player reaches level 3 (optional, can be removed if not using different backgrounds for levels)
            level3Background.SetActive(true); // Show the level 3 background when the player reaches level 3 (optional, can be removed if not using different backgrounds for levels)

            LevelUp("LEVEL 3"); // Call the LevelUp method to handle level progression and display the new level (optional, can be removed if not using levels or a level display)
        }

        if (score >= 120 && currentLevel == 3)
        {
            currentLevel++; // Increase the level when the score reaches 300 (optional, can be used for increasing difficulty or other level-based features
            level3Background.SetActive(false); // Hide the level 3 background when the player reaches level 4 (optional, can be removed if not using different backgrounds for levels)
            level4Background.SetActive(true); // Show the level 4 background when the player reaches level 4 (optional, can be removed if not using different backgrounds for levels)

            LevelUp("LEVEL 4"); // Call the LevelUp method to handle level progression and display the new level (optional, can be removed if not using levels or a level display)

        }

        if (score >= 160 && currentLevel == 4)
        {
            currentLevel++; // Increase the level when the score reaches 400 (optional, can be used for increasing difficulty or other level-based features)
            level4Background.SetActive(false); // Hide the level 4 background when the player reaches level 5 (optional, can be removed if not using different backgrounds for levels)
            level5Background.SetActive(true); // Show the level 5 background when the player reaches level 5 (optional, can be removed if not using different backgrounds for levels)

            LevelUp("LEVEL 5"); // Call the LevelUp method to handle level progression and display the new level (optional, can be removed if not using levels or a level display)
        }

        if (score >= 200 && currentLevel == 5)
        {
            GameWin(); // Call the GameWin method to handle winning the game when the player reaches a certain score (optional, can be removed if not implementing a win condition based on score)
            return;
        }
    }
    public void AddScore(int points)
    {
        score += points; // Increase score by the specified points
        scoreText.text = "Score: " + score; // Update the score text to reflect the new score
        CheckLevelProgression(); // Check if the player has reached a score threshold for level progression (optional, can be removed if not using levels)

        if (goodCatchSound != null)
        {
            sfxSource.PlayOneShot(goodCatchSound); // Play the good catch sound effect when the player catches a good item (optional, can be removed if not using sound effects)
            CheckLevelProgression(); // Check if the player has reached a score threshold for level progression (optional, can be removed if not using levels)
        }
    }

    public void LoseLife()
    {
        if (isGameOver)
        {
            return;
        }

        lives--; // Decrease lives by one
        UpdateHeartsWithAnimation(); // Update the heart icons with animation when the player loses a life (optional, can be removed if not using heart icons for lives)

        livesText.text = "Lives: "; 

        if (badCatchSound != null)
        {
                sfxSource.PlayOneShot(badCatchSound); // Play the bad catch sound effect when the player catches a bad item (optional, can be removed if not using sound effects)
        }   

        if (lives <= 0)
        {
            lives = 0; // Ensure lives do not go below zero
            isGameOver = true; // Set game over flag when lives run out
            
            musicSource.Stop(); // Stop the background music when the game is over (optional, can be removed if you want the music to continue during the game over screen)

            if (gameOverSound != null)
             {
                    sfxSource.PlayOneShot(gameOverSound); // Play the game over sound effect when the game is over (optional, can be removed if not using sound effects)
            }

            FindFirstObjectByType<ItemSpawner>().StopSpawning(); // Stop spawning items when the game is over

            cart.SetActive(false); // Hide the cart when the game is over (optional, can be removed if you want the cart to remain visible during the game over screen)
            level1Background.SetActive(false); // Hide the level 1 background when the game is over (optional, can be removed if not using different backgrounds for levels)
            level2Background.SetActive(false); // Hide the level 2 background when the game is over (optional, can be removed if not using different backgrounds for levels)
            level3Background.SetActive(false); // Hide the level 3 background when the game is over (optional, can be removed if not using different backgrounds for levels)
            
            scoreText.gameObject.SetActive(false); // Hide the score text when the game is over (optional, can be removed if you want the score to remain visible during the game over screen)
            scorePanel.gameObject.SetActive(false); // Hide the score panel when the game is over (optional, can be removed if not using a score panel)
            livesText.gameObject.SetActive(false); // Hide the lives text when the game is over (optional, can be removed if you want the lives to remain visible during the game over screen)
            livesPanel.gameObject.SetActive(false); // Hide the lives panel when the game is over (optional, can be removed if not using a lives panel)
            levelPanel.gameObject.SetActive(false); // Hide the level panel when the game is over (optional, can be removed if not using a level panel)
            levelText.gameObject.SetActive(false); // Hide the level text when the game is over (optional, can be removed if you want the level to remain visible during the game over screen)

            gameOverBackground.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text = "Final Score: " + score;
            gameOverPanel.SetActive(true);
            restartButton.SetActive(true);

            
            
        }
    }
   

    public void RestartGame()
        {
        SceneManager.LoadScene("Gameplay"); // Reload the current scene to restart the game
    }

        // Update is called once per frame
        void Update()
        {

        }
    }

