using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateDumbbel : MonoBehaviour
{
    int atk;
    public GameObject effect;

    int count;

    public void SettingObject(int ATK, GameObject atkEffect)
    {
        atk = ATK;
        effect = atkEffect;
        Debug.Log(effect);

        Invoke("DestroyItem", 5);
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (count >= 7) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            EnemyStatus enemy = other.GetComponent<EnemyStatus>();
            enemy.GetDamage(atk);
        }
        GameObject e = Instantiate(effect, transform.position, Quaternion.identity);
        e.AddComponent<DestroyEffect>().DestroyTime(1);

        count++;
    }
}
