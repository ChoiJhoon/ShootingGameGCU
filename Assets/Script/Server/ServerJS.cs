using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class RankMain : MonoBehaviour
{
    public string host;
    public int port;
    public string top3Uri;
    public string idUri;
    public string postUri;

    public NickName UserNickname;

    public Button btnGetTop3;
    public Button btnGetId;
    public Button btnPost;

    void Start()
    {
        if (btnGetTop3 == null)
        {
            Debug.LogError("btnGetTop3 is not assigned.");
            return;
        }
        if (btnGetId == null)
        {
            Debug.LogError("btnGetId is not assigned.");
            return;
        }
        if (btnPost == null)
        {
            Debug.LogError("btnPost is not assigned.");
            return;
        }
        if (UserNickname == null)
        {
            Debug.LogError("UserNickname is not assigned.");
            return;
        }

        btnGetTop3.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
            Debug.Log(url);

            StartCoroutine(GetTop3(url, (raw) =>
            {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.result.Length);
                foreach (var user in res.result)
                {
                    Debug.LogFormat("{0} : {1}", user.id, user.score);
                }
            }));
        });

        btnGetId.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, idUri);
            Debug.Log(url);

            StartCoroutine(GetId(url, (raw) => {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_id>(raw);
                Debug.LogFormat("{0}, {1}", res.result.id, res.result.score);
            }));
        });

        btnPost.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, postUri);
            Debug.Log(url); //http://localhost:3030/scores

            var req = new Protocols.Packets.req_scores();
            req.cmd = 1000; //(int)Protocols.eType.POST_SCORE;
            req.id = UserNickname.playerName;
            req.score = ScoreUI.score;
            //req.score = scriptableObject.score;
            // 직렬화  (오브젝트 -> 문자열)
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);
            //{"id":"hong@nate.com","score":100,"cmd":1000}

            StartCoroutine(PostScore(url, json, (raw) => {
                Protocols.Packets.res_scores res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetId(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("--->" + webRequest.downloadHandler.text);

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json); // 직렬화 (문자열 -> 바이트 배열)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }
}
