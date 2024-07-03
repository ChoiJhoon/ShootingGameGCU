using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class top3Score : MonoBehaviour
{
    
    public void Top10Btn()
    {
        Application.OpenURL("http://localhost:3030/scores/top10");
    }
}
