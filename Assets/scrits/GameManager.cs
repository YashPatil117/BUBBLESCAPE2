using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro UI elements

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public TextMeshProUGUI restartText; // Reference to the restart text UI element
    public GameObject player; // Reference to the player object
    private bool isGameOver = false;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy this object if an instance already exists
        }
    }

    void Start()
    {
        // Hide the restart text at the start
        if (restartText != null)
        {
            restartText.gameObject.SetActive(false); // Disable the restart text
        }
        else
        {
            Debug.LogWarning("Restart text not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Check if the player is dead
        if (player == null && !isGameOver)
        {
            GameOver();
        }

        // Restart the game if R is pressed after game over
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Call this method to trigger the game over state
    public void GameOver()
    {
        Debug.Log("Game Over");
        if (restartText != null)
        {
            restartText.gameObject.SetActive(true); // Show the restart text
        }
        isGameOver = true;
    }

    // Restart the game by reloading the scene
    void RestartGame()
    {
        if (restartText != null)
        {
            restartText.gameObject.SetActive(false); // Hide the restart text
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        isGameOver = false; // Reset the game over flag
    }
}
