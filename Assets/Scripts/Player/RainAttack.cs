using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAttack : MonoBehaviour
{
    int atk;
    GameObject effect;

    public void SettingObject(int ATK)
    {
        atk = ATK;

        Invoke("DestroyObject", 6f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyStatus enemy = other.GetComponent<EnemyStatus>();
            enemy.GetDamage(atk);
        }
    }
}
