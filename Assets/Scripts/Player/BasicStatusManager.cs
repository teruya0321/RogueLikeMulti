using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStatusManager : MonoBehaviour
{
    GameManager gameManager;

    public const int defaultHP = 10;

    public const int defaultATK = 3;

    public const int defaultSPEED = 3;

    public int HP;
    public int ATK;
    public int SPEED;

    public int basicRestPoint;

    private void Start()
    {
        HP = defaultHP;
        ATK = defaultATK;
        SPEED = defaultSPEED;

        gameManager = GetComponent<GameManager>();
    }
    
    public void ChangeHP()
    {
        List<Transform> pList = gameManager.players;
        foreach (Transform p in pList )
        {
            p.gameObject.GetComponent<PlayerStatus>().MaxHP = HP;
            p.gameObject.GetComponent<PlayerStatus>().HP = HP - p.gameObject.GetComponent<PlayerStatus>().getDamage;

            p.gameObject.GetComponent<PlayerStatus>().slider.maxValue = HP;
        }

    }

    public void ChangeATK()
    {
        List<Transform> pList = gameManager.players;
        foreach(Transform p in pList)
        {
            p.gameObject.GetComponent<PlayerStatus>().ATK = ATK;
        }
    }

    public void ChangeSPEED()
    {
        List<Transform> pList = gameManager.players;
        foreach( Transform p in pList)
        {
            p.gameObject.GetComponent<PlayerStatus>().SPEED = SPEED;
        }
    }
}
