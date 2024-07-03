using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using Newtonsoft.Json;

public class top3Score : MonoBehaviour
{
    private const string TOP3_URL = "http://localhost:3030/scores/top3";

    [System.Serializable]
    public class ScoreData
    {
        public string _id; // MongoDB에서는 _id가 string 형태로 반환됩니다.
        public int score;
    }

    [System.Serializable]
    public class Top3Response
    {
        public int cmd;
        public string message;
        public ScoreData[] result;
    }

    public TMP_Text top3Text;

    void Start()
    {
        StartCoroutine(GetTop3ScoresCoroutine());
    }

    private IEnumerator GetTop3ScoresCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(TOP3_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Top3Response response = JsonConvert.DeserializeObject<Top3Response>(jsonResponse);

                if (response.cmd == 1101)
                {
                    string top3String = "Top 3 Scores:\n";

                    foreach (var scoreData in response.result)
                    {
                        top3String += $"{scoreData._id}: {scoreData.score}\n";
                    }

                    top3Text.text = top3String;
                }
                else
                {
                    Debug.LogWarning("Unexpected response: " + jsonResponse);
                }
            }
        }
    }
}
