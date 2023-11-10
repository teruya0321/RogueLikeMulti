using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    GameManager gameManager;

    List<Transform> playerList = new List<Transform>();

    List<Transform> playerSpawnPos = new List<Transform>();

    public LoadingImage loadingImageScript;

    public EnemyManager enemyManager;
    void Start()
    {
        playerList = GameManager.MainGameManager.players;

        foreach (Transform p in transform) playerSpawnPos.Add(p);

        gameManager = GameManager.MainGameManager;

        MovePlayers();
    }
    public void MovePlayers()
    {
        gameManager.situation = GameManager.Situation.Loading;

        int i = Random.Range(0, playerSpawnPos.Count);
        PlayerPrefs.SetInt("SpawnPointInt", i);

        loadingImageScript.gameObject.SetActive(true);
        loadingImageScript.StartCoroutine("SettingImage");

        foreach (Transform t in playerList)
        {
            t.transform.position = playerSpawnPos[i].position + new Vector3(Random.Range(0,6),0,Random.Range(0,6));

            t.GetComponent<PlayerMove>().loading = true;

            //enemyManager.SummonEnemy(i);
        }
    }
}
