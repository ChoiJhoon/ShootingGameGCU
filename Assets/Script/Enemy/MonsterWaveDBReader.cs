using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class MonsterWaveDBReader : MonoBehaviour
{
    [SerializeField]
    private GameObject Boss;
    [SerializeField]
    private GameObject SuicideMob;
    [SerializeField]
    private GameObject ShootingMob;

    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private Transform bossSpawn;

    public class WaveData
    {
        public int stage;
        public int time;
        public int SuicideBombing;
        public int Shooting;
        public int boss;
    }

    // CSV ������ �о �����͸� ������ ����Ʈ�Դϴ�.
    public List<WaveData> waveDataList;

    void Start()
    {
        LoadCSV();
        StartCoroutine(StartStages());
    }

    void LoadCSV()
    {
        waveDataList = new List<WaveData>();

        // Resources �������� CSV ������ �о�ɴϴ�.
        TextAsset csvFile = Resources.Load<TextAsset>("MonsterWaveDB");
        if (csvFile != null)
        {
            using (StringReader reader = new StringReader(csvFile.text))
            {
                bool isHeader = true;
                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    if (isHeader)
                    {
                        isHeader = false;
                        continue; // ����� �ǳʶݴϴ�.
                    }

                    var values = line.Split(',');

                    WaveData waveData = new WaveData
                    {
                        stage = int.Parse(values[0]),
                        time = int.Parse(values[1]),
                        SuicideBombing = int.Parse(values[2]),
                        Shooting = int.Parse(values[3]),
                        boss = int.Parse(values[4])
                    };

                    waveDataList.Add(waveData);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found.");
        }
    }

    IEnumerator StartStages()
    {
        foreach (var waveData in waveDataList)
        {
            Debug.Log($"Stage: {waveData.stage} - Starting");
            StartCoroutine(SpawnMonsters(waveData));

            // ���� �ð� ���� ���͸� �����մϴ�.
            yield return new WaitForSeconds(waveData.time);

            // ���� ����
            Debug.Log($"Stage: {waveData.stage} - Boss appears!");
            GameObject boss = Instantiate(Boss, bossSpawn.position, bossSpawn.rotation);
            BossScript bossController = boss.GetComponent<BossScript>();
            BossScript.OnBossDefeated += () => OnBossDefeated(waveData.stage);
        }
    }

    IEnumerator SpawnMonsters(WaveData waveData)
    {
        float spawnInterval = waveData.time / (waveData.SuicideBombing + waveData.Shooting);

        for (int i = 0; i < waveData.SuicideBombing; i++)
        {
            SpawnMonster(SuicideMob);
            yield return new WaitForSeconds(spawnInterval);
        }

        for (int i = 0; i < waveData.Shooting; i++)
        {
            SpawnMonster(ShootingMob);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster(GameObject monsterPrefab)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return;
        }

        // ������ ���� ����Ʈ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Debug.Log($"Spawning {monsterPrefab.name} at {spawnPoint.position}");
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void OnBossDefeated(int stage)
    {
        Debug.Log($"Stage: {stage} - Boss defeated!");
        // ���� ���������� �Ѿ�ϴ�.
        StartCoroutine(StartStages());
    }
}
