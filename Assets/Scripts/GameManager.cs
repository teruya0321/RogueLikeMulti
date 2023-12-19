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

    RoundCount roundcounter;

    public SettingStatusManager settingStatusManager;

    public BasicStatusManager basicStatusManager;

    public AudioSource audioSource;
    public AudioClip[] clips;

    public enum Situation
    {
        Loading,
        SettingStatus,
        SpawnTime,
        Battle,
        Other
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

        situation = Situation.Other;

        roundcounter = GetComponent<RoundCount>();

        basicStatusManager = GetComponent<BasicStatusManager>();

        audioSource = GetComponent<AudioSource>();
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
                //Debug.Log(situation);
                /*mainCamera.transform.localEulerAngles = new Vector3
                    (mainCamera.transform.localEulerAngles.x,
                    players[0].transform.localEulerAngles.y,
                    mainCamera.transform.localEulerAngles.z);*/
                break;

            case Situation.SettingStatus:
                //Debug.Log(situation);
                break;

            case Situation.SpawnTime:
                enemyManager.SummonEnemy();
                situation = Situation.Battle;
                break;

            case Situation.Battle:
                //Debug.Log(situation);
                break;

            case Situation.Other:
                //Debug.Log(situation);
                break;

            default:
                //Debug.Log(situation);
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
        DontDestroyOnLoad(gameObject);

        foreach (Transform t in players)
        {
            DontDestroyOnLoad(t.gameObject);
        }

        DontDestroyOnLoad(mainCamera);

        DontDestroyOnLoad(centerObj);

        SceneManager.LoadScene("Main");
    }

    public RoundCount ReturnRoundCount()
    {
        return roundcounter;
    }
}
