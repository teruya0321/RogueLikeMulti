using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerNotice : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.MainGameManager.players.Count == 0) gameObject.SetActive(true);

        else gameObject.SetActive(false);
    }
}
