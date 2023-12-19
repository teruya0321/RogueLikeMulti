using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyAI enemyAI;

    public int HP;
    public int ATK;
    int SPEED;

    public int AtkTime = 3;
    float attackTimer;

    public GameObject effect;

    public MonoBehaviour ability;
    
    void Start()
    {
        enemyAI.RequestAgent().speed = SPEED * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > AtkTime * 10 && enemyAI.target != null)
        {
            EnemyAttack();
        }

        if (HP <= 0)
        {
            Debug.Log("グエー死んだンゴ");

            int i = Random.Range(0, 10);

            Debug.Log(i);

            if (i > 6)
            {
                Instantiate((GameObject)Resources.Load("Prefab/Item" + Random.Range(1,5)), transform.position + transform.up, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    void EnemyAttack()
    {
        if (ability != null) return;

        RaycastHit hit;

        Debug.DrawRay(gameObject.transform.position + Vector3.up, gameObject.transform.forward * enemyAI.RequestAgent().stoppingDistance, Color.red);
        if(Physics.Raycast(gameObject.transform.position + Vector3.up, gameObject.transform.forward, out hit,enemyAI.RequestAgent().stoppingDistance))
        {
            //Debug.Log("test1");
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit");
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(((Vector3.forward * 4) + Vector3.up) * 10, ForceMode.Impulse);

                hit.collider.gameObject.GetComponent<PlayerStatus>().PlayerGetDamage(ATK);

                GameObject e = Instantiate(effect, hit.collider.gameObject.transform.position, Quaternion.identity);
                e.AddComponent<ObjectFalse>().destroyTime = 2;

                attackTimer = 0;
            }
        }
    }

    public void GetDamage(int damage)
    {
        int nowhp = HP;
        HP -= damage;

        if (nowhp < HP) Debug.Log("Hpを" + (HP - nowhp) + "回復したぜ！");
        //Debug.Log(HP);
    }

    public void SettingStatus(int getHP,int getATK,int getSP,int getRate,bool ATKtype)
    {
        HP = getHP;
        ATK = getATK;
        SPEED = getSP;

        AtkTime = getRate;

        enemyAI = GetComponent<EnemyAI>();
        //Debug.Log(enemyAI);

        if (!ATKtype) enemyAI.RequestAgent().stoppingDistance = 2;
        else enemyAI.RequestAgent().stoppingDistance = 7;
    }

    public void SettingAbility(MonoBehaviour getAbility)
    {
        ability = getAbility;
    }
}
