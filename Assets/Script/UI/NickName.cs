using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NickName : MonoBehaviour
{
    public TMP_InputField nicknameTMPInputField; // Text Area�� Inspector���� �Ҵ��ؾ� �մϴ�.
    private string PlayerName = null;

    // ���� ���� ��ư�� ���� �� ȣ���� �޼���
    public void OnGameStartButtonPressed()
    {
        InputName(); // �г����� �Է��ϸ� �ٷ� �����ϵ��� ȣ��
    }

    private void InputName()
    {
        // TMP Input Field�� null�� ��� ���� �α� ��� �� �޼��� ����
        if (nicknameTMPInputField == null)
        {
            Debug.LogError("TMP_InputField is not assigned in the Inspector.");
            return;
        }

        PlayerName = nicknameTMPInputField.text;
        PlayerPrefs.SetString("CurrentPlayerName", PlayerName);

        // SpawnManagerScriptableObject.Instance�� null�� �ƴ��� Ȯ�� �� ȣ��
        if (SpawnManagerScriptableObject.Instance != null)
        {
            SpawnManagerScriptableObject.Instance.ScoreSet(SpawnManagerScriptableObject.Instance.score, PlayerName);
        }
        else
        {
            Debug.LogError("SpawnManagerScriptableObject.Instance is null. Make sure the resource path is correct.");
            return; // ���� �߻� �� �޼��� ����
        }

        Debug.Log("Entered nickname: " + PlayerName);
    }
}
