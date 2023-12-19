using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    EnemyStatus enemyStatus;

    bool bom;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        enemyStatus.SettingAbility(this);

        GetComponent<EnemyAI>().RequestAgent().stoppingDistance = 0;
    }

    private void Update()
    {
        if (!bom) return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 7)
        {
            GameObject player = collision.gameObject;

            player.GetComponent<PlayerStatus>().PlayerGetDamage(enemyStatus.ATK);

            GameObject e = Instantiate((GameObject)Resources.Load("Effect/Explosion"),transform.position,Quaternion.identity);
            e.AddComponent<ObjectFalse>().destroyTime = 2;

            Destroy(gameObject);
        }
    }


}
