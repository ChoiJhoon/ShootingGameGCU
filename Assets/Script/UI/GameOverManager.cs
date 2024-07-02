using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Optional: Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
