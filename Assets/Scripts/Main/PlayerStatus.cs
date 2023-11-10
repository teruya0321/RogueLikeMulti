using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    int HP;
    int ATK;
    int SPEED;

    float maxDistance;



    private void Start()
    {
        
    }

    void PlayerAttacked()
    {
        RaycastHit hit;

        if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward, out hit, 7))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyAI>().GetDamage(ATK);
            }
        }
    }

    public void PlayerGetDamage(int damage)
    {
        HP -= damage;
    }
}
