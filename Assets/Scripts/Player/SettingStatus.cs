using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingStatus : MonoBehaviour
{
    public int statusPoint;

    PlayerStatus status;

    public GameObject statusUI;

    public GameObject hostStatus;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;
        if (statusUI == null) return;

        if(statusPoint > 0) statusUI.SetActive(true);
        else statusUI.SetActive(false);
    }

    public void OnA()
    {
        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;
        if (statusPoint <= 0) return;

        status.healing++;

        statusPoint--;
    }

    public void OnB()
    {
        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;
        if (statusPoint <= 0) return;

        
        status.wisdom++;

        statusPoint--;
    }

    public void OnX()
    {
        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;
        if (statusPoint <= 0) return;

        status.shoatRange++;

        statusPoint--;
    }

    public void OnY()
    {
        if (GameManager.MainGameManager.situation != GameManager.Situation.SettingStatus) return;
        if (statusPoint <= 0) return;
        status.longRange++;


        statusPoint--;
    }
}
