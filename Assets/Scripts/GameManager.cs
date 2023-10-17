using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject centerObj;

    public GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject player = Instantiate(playerObj);

            //OnplayerJoined(player.GetComponent<PlayerInput>());
        }
    }

    public void OnplayerJoined(PlayerInput input)
    {
        centerObj.GetComponent<TargetTest>().JoinPlayer(input.gameObject);
    }

    public void OnplayerLeft(PlayerInput input)
    {
        centerObj.GetComponent<TargetTest>().LeftPlayer(input.gameObject);
    }
}
