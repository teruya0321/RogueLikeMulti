using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;

    public List<GameObject> enemyList = new List<GameObject>();

    public PlayerSpawnManager spawnManager;

    List<Transform> enemySpawnPoints = new List<Transform>();

    bool countStop;

    private void Awake()
    {
        GameManager.MainGameManager.enemyManager = this;
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
            spawnManager.Invoke("MovePlayers", 2);

            countStop = true;
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
        int spawnCount = Random.Range(5, 10);

        for (int i = 0; i <= spawnCount; i++)
        {
            Instantiate(enemy,
                enemySpawnPoints[PlayerPrefs.GetInt("SpawnPoint")].position + new Vector3(Random.Range(-5,6),0,Random.Range(-5,6)),
                Quaternion.identity);
        }
    }
}
