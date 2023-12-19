using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    GameManager gameManager;

    List<Transform> playerList = new List<Transform>();

    List<Transform> playerSpawnPos = new List<Transform>();

    public Transform bossPlayerSpawn;

    public LoadingImage loadingImageScript;

    public EnemyManager enemyManager;

    private void Awake()
    {
        gameManager = GameManager.MainGameManager;
    }
    void Start()
    {
        playerList = GameManager.MainGameManager.players;

        foreach (Transform p in transform) playerSpawnPos.Add(p);

        gameManager = GameManager.MainGameManager;

        MovePlayers();
    }
    public void MovePlayers()
    {
        Debug.Log("移動しているんだが?");
        gameManager.situation = GameManager.Situation.Loading;

        string getStageNumber = GameManager.MainGameManager.ReturnRoundCount().ReturnCount().ToString("00000");

        //Debug.Log("ステージ" +  getStageNumber);
        if (getStageNumber[getStageNumber.Length - 1] == '0' || getStageNumber[getStageNumber.Length - 1] == '5')
        {
            //Debug.Log("ボス戦なんだが？");

            PlayerPrefs.SetInt("SpawnInt", -1);

            loadingImageScript.gameObject.SetActive(true);

            foreach(Transform t in playerList)
            {
                t.transform.position = bossPlayerSpawn.position + new Vector3(Random.Range(0, 6), 0, Random.Range(0, 6));

                t.GetComponent<PlayerMove>().loading = true;
            }

            gameManager.audioSource.clip = gameManager.clips[1];
            gameManager.audioSource.Play();
        }

        else
        {
            int i = Random.Range(0, playerSpawnPos.Count);
            PlayerPrefs.SetInt("SpawnInt", i);

            loadingImageScript.gameObject.SetActive(true);

            foreach (Transform t in playerList)
            {
                t.transform.position = playerSpawnPos[i].position + new Vector3(Random.Range(0, 6), 0, Random.Range(0, 6));

                t.GetComponent<PlayerMove>().loading = true;

                //enemyManager.SummonEnemy(i);
            }

            gameManager.audioSource.clip = gameManager.clips[0];
            gameManager.audioSource.Play();
        }
    }
}
