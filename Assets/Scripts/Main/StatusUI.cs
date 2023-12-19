using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    GameObject player;

    public Text shoatRangeText;
    public Text longRangeText;
    public Text healingText;
    public Text wisdomText;

    public Text restPointText;

    private void LateUpdate()
    {
        if (player == null) return;

        shoatRangeText.text = "" + player.GetComponent<PlayerStatus>().shoatRange;
        longRangeText.text = "" + player.GetComponent<PlayerStatus>().longRange;
        healingText.text = "" + player.GetComponent<PlayerStatus>().healing;
        wisdomText.text = "" + player.GetComponent <PlayerStatus>().wisdom;

        restPointText.text = "" + player.GetComponent<SettingStatus>().statusPoint;
    }

    public void SettingUI(GameObject setPlayer)
    {
        player = setPlayer;
    }
}
