using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDumbbel : MonoBehaviour
{
    int atk;
    GameObject effect;

    public void SettingObject(int ATK, GameObject atkEffect)
    {
        atk = ATK;
        effect = atkEffect;
        Invoke("DestroyItem", 5);
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject e = Instantiate(effect, transform.position, Quaternion.identity);
        e.AddComponent<DestroyEffect>().DestroyTime(1);
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            enemy.GetDamage(atk);
            Destroy(gameObject);
        }
    }
}
