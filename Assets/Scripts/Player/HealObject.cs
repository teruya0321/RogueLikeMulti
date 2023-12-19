using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    int healingPoint;
    GameObject healingEffect;

    public void SettingPoints(int point,GameObject effect)
    {
        healingPoint = point;
        healingEffect = effect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;

            player.GetComponent<PlayerStatus>().PlayerGetDamage(healingPoint * -1);
            GameObject healEffect = Instantiate(healingEffect,gameObject.transform.position,Quaternion.identity);
            healEffect.AddComponent<DestroyEffect>().DestroyTime(1);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;

            player.GetComponent<PlayerStatus>().PlayerGetDamage(healingPoint * -1);
        }

        Invoke("DestroyObject", 1.5f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
