using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NickName : MonoBehaviour
{
    public TMP_InputField playerIDInputField;
    public Button startGameButton;

    public string playerName = null;

    private void Awake()
    {
        playerName = playerIDInputField.GetComponent<TMP_InputField>().text;
    }

    private void Start()
    {
        // 게임 시작 버튼에 클릭 이벤트 리스너 추가
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        playerName = playerIDInputField.text;
        PlayerPrefs.SetString("CurrentPlayerName", playerName);

        string playerID = playerIDInputField.text;

        // ScriptableObject의 인스턴스에 ID 설정
        SpawnManagerScriptableObject.Instance.ScoreSet(0, playerID);

        // 디버그 로그로 확인
        Debug.Log("Player ID: " + SpawnManagerScriptableObject.Instance.id);
    }
}
