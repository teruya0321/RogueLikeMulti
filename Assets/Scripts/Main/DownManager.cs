using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownManager : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.MainGameManager;
    }
    private void FixedUpdate()
    {
        int count = 0;

        List<Transform> playerList = gameManager.players;
        foreach (Transform player in playerList)
        {
            if(player.GetComponent<PlayerStatus>().HP <= 0)
            {
                count++;
            }
        }

        if (count == playerList.Count) Invoke("SceneMove", 2f);
    }

    void SceneMove()
    {
        SceneManager.LoadScene("End");
    }
}
