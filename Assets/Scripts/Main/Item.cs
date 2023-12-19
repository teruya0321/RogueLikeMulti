using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int number = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));

            if (number == 1) collision.gameObject.GetComponent<PlayerStatus>().HP +=
                    (collision.gameObject.GetComponent<PlayerStatus>().MaxHP - collision.gameObject.GetComponent<PlayerStatus>().HP) / 2;

            if (number == 2) collision.gameObject.GetComponent<PlayerStatus>().MaxHP += 1; 
            collision.gameObject.GetComponent<PlayerStatus>().HP += 1;

            if (number == 3) collision.gameObject.GetComponent<PlayerStatus>().ATK += 1;

            if(number == 4) collision.gameObject.GetComponent<PlayerStatus>().SPEED += 1;
        }
    }
}
