using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject enemy;

    public List<GameObject> enemyList = new List<GameObject>();

    public PlayerSpawnManager spawnManager;

    List<Transform> enemySpawnPoints = new List<Transform>();

    public Transform bossSpawnPoint;

    public List<Transform> bossFollowerSpawnPoint;

    bool countStop = true;

    TextAsset csvFile;
    public List<string[]> csvDatas = new List<string[]>();// CSVの中身を入れるリスト;

    public GameObject atkEffect;

    private void Awake()
    {
        GameManager.MainGameManager.enemyManager = this;

        csvFile = Resources.Load("CSVs/EnemyStatus") as TextAsset;// Resouces下のCSV読み込み

        StringReader reader = new StringReader(csvFile.text);
        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }

    private void Start()
    {
        foreach(Transform e in transform)
        {
            enemySpawnPoints.Add(e);
        }
    }
    public void LateUpdate()
    {
        if(enemyList.Count == 0 && !countStop)
        {
            countStop = true;
            spawnManager.Invoke("MovePlayers", 2);
        }
    }
    public void AddEnemy(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void SummonEnemy()
    {
        if (!countStop) return;

        countStop = false;

        if (PlayerPrefs.GetInt("SpawnInt") == -1)
        {
            BossSummon();
            return;
        }

        int spawnCount = Random.Range(5, 10);

        for (int i = 0; i <= spawnCount; i++)
        {
            int id = Random.Range(1, 7);
            enemy = (GameObject)Resources.Load("Prefab/" + csvDatas[id][1]);



            GameObject enemys = Instantiate(enemy,
                enemySpawnPoints[PlayerPrefs.GetInt("SpawnInt")].position + new Vector3(Random.Range(-5,6),0,Random.Range(-5,6)),
                Quaternion.identity);

            enemys.AddComponent<EnemyAI>();

            EnemyStatus enemyStatus = enemys.AddComponent<EnemyStatus>();
            enemyStatus.SettingStatus(int.Parse(csvDatas[id][2]) + (PlayerPrefs.GetInt("Round")),
                                      int.Parse(csvDatas[id][3]) + (PlayerPrefs.GetInt("Round")), 
                                      int.Parse(csvDatas[id][4]) + (PlayerPrefs.GetInt("Round")), 
                                      int.Parse(csvDatas[id][5]), 
                                      bool.Parse(csvDatas[id][6]));

            AddEnemy(enemys);

            enemyStatus.effect = atkEffect;

            if (csvDatas[id][7] != "None")
            {
                var ability = System.Type.GetType(/*csvDatas[id][7]*/"Explosion");

                Debug.Log(ability);

                enemys.AddComponent(ability);
            }

            // Debug.Log(bool.Parse(csvDatas[id][6]));

            // enemys.AddComponent(System.Type.GetType("Ability/")); 　仮置き ダブルクオーテーションの中に必要なスクリプトの名前を入れる
        }
    }

    void BossSummon()
    {
        int bossid = Random.Range(7, 10);

        //Debug.Log("Prefab/" + int.Parse(csvDatas[bossid][1]));

        GameObject boss = Instantiate((GameObject)Resources.Load("Prefab/" + csvDatas[bossid][1]), bossSpawnPoint.position, Quaternion.identity);

        boss.AddComponent<EnemyAI>();

        boss.AddComponent<AddStatusPoint>();

        EnemyStatus bossStatus = boss.AddComponent<EnemyStatus>();
        bossStatus.SettingStatus(int.Parse(csvDatas[bossid][2]) + (int)(PlayerPrefs.GetInt("Round")),
                                 int.Parse(csvDatas[bossid][3]) + (int)(PlayerPrefs.GetInt("Round") * 5),
                                 int.Parse(csvDatas[bossid][4]) + (int)(PlayerPrefs.GetInt("Round")),
                                 int.Parse(csvDatas[bossid][5]),
                                 bool.Parse(csvDatas[bossid][6]));

        AddEnemy(boss);

        bossStatus.effect = atkEffect;

        int spawnCount = Random.Range(5, 10);

        for (int i = 0; i <= spawnCount * 2; i++)
        {
            int id = Random.Range(1, 7);
            enemy = (GameObject)Resources.Load("Prefab/" + csvDatas[id][1]);



            GameObject enemys = Instantiate(enemy,
                bossFollowerSpawnPoint[Random.Range(0,2)].position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6)),
                Quaternion.identity);

            enemys.AddComponent<EnemyAI>();

            EnemyStatus enemyStatus = enemys.AddComponent<EnemyStatus>();
            enemyStatus.SettingStatus(int.Parse(csvDatas[id][2]) + (int)(PlayerPrefs.GetInt("Round")),
                                      int.Parse(csvDatas[id][3]) + (int)(PlayerPrefs.GetInt("Round") * 2),
                                      int.Parse(csvDatas[id][4]) + (int)(PlayerPrefs.GetInt("Round") * 2),
                                      int.Parse(csvDatas[id][5]),
                                      bool.Parse(csvDatas[id][6]));

            AddEnemy(enemys);

            enemyStatus.effect = atkEffect;

            if (csvDatas[id][7] != "None")
            {
                var ability = System.Type.GetType(/*csvDatas[id][7]*/"Explosion");

                Debug.Log(ability);

                enemys.AddComponent(ability);
            }



            // enemys.AddComponent(System.Type.GetType("Ability/")); 　仮置き ダブルクオーテーションの中に必要なスクリプトの名前を入れる
        }
    }
}
