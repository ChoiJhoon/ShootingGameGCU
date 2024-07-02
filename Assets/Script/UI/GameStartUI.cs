using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private GameObject gameStartUI;

    public void ShowGameStart()
    {
        gameStartUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
