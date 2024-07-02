using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NickName : MonoBehaviour
{
    public TMP_InputField nicknameTMPInputField; // Text Area를 Inspector에서 할당해야 합니다.
    private string PlayerName = null;

    // 게임 시작 버튼을 누를 때 호출할 메서드
    public void OnGameStartButtonPressed()
    {
        InputName(); // 닉네임을 입력하면 바로 저장하도록 호출
    }

    private void InputName()
    {
        // TMP Input Field가 null인 경우 에러 로그 출력 후 메서드 종료
        if (nicknameTMPInputField == null)
        {
            Debug.LogError("TMP_InputField is not assigned in the Inspector.");
            return;
        }

        PlayerName = nicknameTMPInputField.text;
        PlayerPrefs.SetString("CurrentPlayerName", PlayerName);

        // SpawnManagerScriptableObject.Instance가 null이 아닌지 확인 후 호출
        if (SpawnManagerScriptableObject.Instance != null)
        {
            SpawnManagerScriptableObject.Instance.ScoreSet(SpawnManagerScriptableObject.Instance.score, PlayerName);
        }
        else
        {
            Debug.LogError("SpawnManagerScriptableObject.Instance is null. Make sure the resource path is correct.");
            return; // 오류 발생 시 메서드 종료
        }

        Debug.Log("Entered nickname: " + PlayerName);
    }
}
