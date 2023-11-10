using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaMove : MonoBehaviour
{
    GameObject player;

    bool countdown;

    float timer = 10f;

    public Text countText;

    private void Update()
    {
        if (countdown) timer -= Time.deltaTime;

        if (player == null) countdown = false;

        countText.gameObject.SetActive(countdown);

        countText.text = "Ç‹Ç‡Ç»Ç≠ÉQÅ[ÉÄÇ™énÇ‹ÇËÇ‹Ç∑ÅI\n"+ timer.ToString("N2");

        if(timer <= 0) AreaMoving();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            countdown = true;

            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == player)
        {
            countdown = false;

            timer = 10f;
        }
    }

    void AreaMoving()
    {
        GameManager.MainGameManager.CheckPlayer();
    }
}
