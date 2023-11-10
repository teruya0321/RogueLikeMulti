using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager MainGameManager;

    public GameObject mainCamera;

    public GameObject centerObj;

    public GameObject playerObj;

    public List<Transform> players = new List<Transform>();

    int playerNumber = 0;

    public Situation situation;

    public EnemyManager enemyManager;

    public enum Situation
    {
        Loading,
        SettingStatus,
        Battle
    }
    private void Awake()
    {
        CheckGameManager();
    }

    void CheckGameManager()
    {
        if (MainGameManager == null) MainGameManager = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            GameObject player = Instantiate(playerObj);
        }
#endif

        switch (situation)
        {
            case Situation.Loading:
                break;

            case Situation.SettingStatus:
                break;

            case Situation.Battle:
                enemyManager.SummonEnemy();
                break;

            default:
                break;  
        }
    }

    public void OnplayerJoined(PlayerInput input)
    {
        players.Add(input.transform);

        centerObj.GetComponent<TargetTest>().ChangePlayerCount(players);

        playerNumber++;

        input.gameObject.name = "Player" + playerNumber;
    }

    public void OnplayerLeft(PlayerInput input)
    {
        players.Remove(input.transform);

        centerObj.GetComponent<TargetTest>().ChangePlayerCount(players);

        playerNumber--;
    }

    public void CheckPlayer()
    {
        foreach (Transform t in players)
        {
            DontDestroyOnLoad(t.gameObject);
        }

        DontDestroyOnLoad(mainCamera);

        DontDestroyOnLoad(centerObj);

        SceneManager.LoadScene("Main");
    }
}
