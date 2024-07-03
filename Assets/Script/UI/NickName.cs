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
        // ���� ���� ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        playerName = playerIDInputField.text;
        PlayerPrefs.SetString("CurrentPlayerName", playerName);

        string playerID = playerIDInputField.text;

        // ScriptableObject�� �ν��Ͻ��� ID ����
        SpawnManagerScriptableObject.Instance.ScoreSet(0, playerID);

        // ����� �α׷� Ȯ��
        Debug.Log("Player ID: " + SpawnManagerScriptableObject.Instance.id);
    }
}
