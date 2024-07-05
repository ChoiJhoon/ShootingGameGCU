using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    public GameObject rankingContent;
    public GameObject rankingItemPrefab;

    // URL to your server endpoint that returns the top 10 scores
    private string serverURL = "http://localhost:3030/scores/top10";

    void Start()
    {
        StartCoroutine(GetTop10Scores());
    }

    IEnumerator GetTop10Scores()
    {
        UnityWebRequest www = UnityWebRequest.Get(serverURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch data: " + www.error);
        }
        else
        {
            try
            {
                // Parse JSON response
                string jsonResponse = www.downloadHandler.text;
                ScoresList scoresList = JsonUtility.FromJson<ScoresList>(jsonResponse);

                if (scoresList == null || scoresList.scores == null)
                {
                    Debug.LogError("Failed to parse JSON: scores list is null");
                }
                else
                {
                    List<Score> scores = scoresList.scores;
                    DisplayTop10(scores);
                }
            }
            catch (ArgumentException e)
            {
                Debug.LogError("JSON parse error: " + e.Message);
            }
        }
    }


    public void DisplayTop10(List<Score> scores)
    {
        // Clear existing ranking items
        foreach (Transform child in rankingContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new ranking items
        foreach (Score score in scores)
        {
            GameObject rankingItem = Instantiate(rankingItemPrefab, rankingContent.transform);
            rankingItem.transform.Find("PlayerName").GetComponent<Text>().text = score._id;
            rankingItem.transform.Find("Score").GetComponent<Text>().text = score.score.ToString();
            rankingItem.transform.Find("PlayTime").GetComponent<Text>().text = score.playTime;
        }
    }

    [System.Serializable]
    public class Score
    {
        public string _id;
        public int score;
        public string playTime;
    }

    [System.Serializable]
    private class ScoresList
    {
        public List<Score> scores;
    }
}
