using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            PlayerStatus player = transform.root.gameObject.GetComponent<PlayerStatus>();
            collision.gameObject.GetComponent<EnemyStatus>().GetDamage(player.ATK + (player.healing / 2) + PlayerPrefs.GetInt("Round"));

            collision.rigidbody.AddForce(collision.transform.forward * -1 + collision.transform.up);
        }
    }
}
