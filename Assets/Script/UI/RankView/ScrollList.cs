using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    public GameObject panelPrefab;  // 패널 Prefab
    public Transform content;       // Content Transform
    public int poolSize = 10;       // 풀 크기

    private Queue<GameObject> panelPool = new Queue<GameObject>();

    void Start()
    {
        InitializePool();
        StartCoroutine(FetchScores());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject panel = Instantiate(panelPrefab, content);
            panel.SetActive(false);
            panelPool.Enqueue(panel);
        }
    }

    IEnumerator FetchScores()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3030/scores/Top10");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            List<Score> scores = JsonUtility.FromJson<ScoreList>("{\"scores\":" + www.downloadHandler.text + "}").scores;
            PopulateList(scores);
        }
    }

    void PopulateList(List<Score> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            GameObject panel = GetPanelFromPool();
            if (panel != null)
            {
                panel.GetComponentInChildren<Text>().text = (i + 1).ToString() + "위 - " + scores[i].score;
                panel.SetActive(true);
                panel.transform.SetSiblingIndex(i);  // 패널 순서를 설정합니다.
            }
        }
    }

    GameObject GetPanelFromPool()
    {
        if (panelPool.Count > 0)
        {
            return panelPool.Dequeue();
        }
        else
        {
            Debug.LogWarning("패널 풀에 더 이상 사용 가능한 패널이 없습니다.");
            return null;
        }
    }

    void ReturnPanelToPool(GameObject panel)
    {
        panel.SetActive(false);
        panelPool.Enqueue(panel);
    }
}

[System.Serializable]
public class Score
{
    public string _id;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<Score> scores;
}
