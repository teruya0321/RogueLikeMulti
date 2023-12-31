using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaMove : MonoBehaviour
{
    GameObject player;

    bool countdown;

    public float timer = 10f;

    public Text countText;

    private void Update()
    {
        if (countdown) timer -= Time.deltaTime;

        if (player == null) countdown = false;

        countText.gameObject.SetActive(countdown);

        countText.text = "まもなくゲームが始まります！\n"+ timer.ToString("N2");

        if (timer <= 0)
        {
            AreaMoving();
            timer = 0;
        }
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
