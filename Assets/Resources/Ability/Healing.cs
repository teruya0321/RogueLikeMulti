using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public List<GameObject> healList = new List<GameObject>();
    EnemyAI enemyAI;
    EnemyStatus enemyStatus;
    GameObject healEffect;

    int Atk;
    float AtkRate;

    float atktimer;

    bool check = false;
    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyStatus = GetComponent<EnemyStatus>();
        if (enemyStatus != null) SetStatus();
        healEffect = (GameObject)Resources.Load("Effect/EnemyHeal");
    }
    void SetStatus()
    {
        Atk = enemyStatus.ATK;
        AtkRate = enemyStatus.AtkTime;
    }

    private void Update()
    {
        if (!check) return;

        atktimer += Time.deltaTime;
        if(atktimer >= AtkRate)
        {
            if (enemyAI.RequestAgent().stoppingDistance >= Vector3.Distance(transform.position, enemyAI.target.position))
                TargetHeal(enemyAI.target.gameObject);
        }
    }
    public void EnemyHealing()
    {
        
        //enemyList.Clear();
        healList = GameManager.MainGameManager.enemyManager.enemyList;

        float targetRange = 500;
        foreach (GameObject enemy in healList)
        {
            float f = Vector3.Distance(transform.position, enemy.transform.position);
            
            if(f < targetRange && enemy.GetComponent<Healing>() == null)
            {
                targetRange = f;
                enemyAI.EnemyTargetSet(enemy.transform);
                //Debug.Log(enemy);
            }
        }
    }

    public void TargetHeal(GameObject target)
    {
        target.GetComponent<EnemyStatus>().GetDamage(Atk * -1);
        atktimer = 0;
    }
}
