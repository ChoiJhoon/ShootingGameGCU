using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public string id;
    public int score;

    private static SpawnManagerScriptableObject _instance;

    public static SpawnManagerScriptableObject Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<SpawnManagerScriptableObject>("Data");
            }
            return _instance;
        }
    }

    public void ScoreSet(int newScore, string playerId)
    {
        score = newScore;
        id = playerId;
    }
}
