using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingStatusManager : MonoBehaviour
{
    public List<Transform> players = new List<Transform>();

    List<GameObject> uiList = new List<GameObject>();

    public int allPlayerPoint;

    public bool havingPoint;

    public GameObject statusUI;
    
    

    private void Awake()
    {
        players = GameManager.MainGameManager.players;

        GameManager.MainGameManager.settingStatusManager = this;
    }

    private void Start()
    {
        foreach (Transform t in players)
        {
            GameObject ui = Instantiate(statusUI, transform);
            ui.gameObject.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value,0.2f);
            uiList.Add(ui);

            ui.GetComponent<StatusUI>().SettingUI(t.gameObject);

            t.gameObject.GetComponent<SettingStatus>().statusUI = ui;

            ui.SetActive(false);
        }
    }

    private void Update()
    {
        foreach(Transform player in players)
        {
            allPlayerPoint += player.GetComponent<SettingStatus>().statusPoint;
        }

        if(allPlayerPoint > 0) havingPoint = true;
        else havingPoint = false;

        allPlayerPoint = 0;

        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;

        if (!havingPoint)
        {
            GameManager.MainGameManager.situation = GameManager.Situation.Battle;

            GameManager.MainGameManager.enemyManager.SummonEnemy();
        }
    }

    public void AllotmentPoint()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.GetComponent<SettingStatus>().statusPoint += PlayerPrefs.GetInt("Round");
        }

        GameManager.MainGameManager.basicStatusManager.basicRestPoint += PlayerPrefs.GetInt("Round") * 2;
    }
}
