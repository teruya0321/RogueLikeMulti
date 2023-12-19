using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingBasicStatusUI : MonoBehaviour
{
    GameManager gameManager;
    BasicStatusManager basicStatusManager;

    public Text hpText;
    public Text atkText;
    public Text speedText;

    public Text restPointText;

    public GameObject selector;

    GameObject hostPlayer;
    PlayerInput hostInput;

    InputAction left;
    InputAction right;
    InputAction selected;

    int number;

    public GameObject allParent;

    private void Start()
    {
        gameManager = GameManager.MainGameManager;

        hostPlayer = GameManager.MainGameManager.players[0].gameObject;
        hostInput = hostPlayer.GetComponent<PlayerInput>();

        left = hostInput.actions.FindAction("L");
        right = hostInput.actions.FindAction("R");
        selected = hostInput.actions.FindAction("StickButton");

        basicStatusManager = GameManager.MainGameManager.basicStatusManager;
    }

    private void Update()
    {
        if(gameManager.situation == GameManager.Situation.SettingStatus && basicStatusManager.basicRestPoint > 0) allParent.SetActive(true);
        else allParent.SetActive(false);

        if (gameManager.situation != GameManager.Situation.SettingStatus) return;

         if(number <= 0) number = 0;
         if(number >= 2) number = 2;

        if (left.WasPressedThisFrame()) number--;
        if (right.WasPressedThisFrame()) number++;

        switch(number)
        {
            case 0:
                selector.transform.position = hpText.transform.position;
                if (selected.WasPressedThisFrame())
                {
                    basicStatusManager.HP++;
                    basicStatusManager.basicRestPoint -= 3;
                    basicStatusManager.ChangeHP();
                }
                break;

            case 1:
                selector.transform.position = atkText.transform.position;
                if (selected.WasPressedThisFrame())
                {
                    basicStatusManager.ATK++;
                    basicStatusManager.basicRestPoint--;
                    basicStatusManager.ChangeATK();
                }
                break;

            case 2:
                selector.transform.position = speedText.transform.position;
                if(selected.WasPressedThisFrame())
                {
                    basicStatusManager.SPEED += 3;
                    basicStatusManager.basicRestPoint--;
                    basicStatusManager.ChangeSPEED();
                }
                break;
        }
    }

    private void LateUpdate()
    {
        if (hostPlayer == null) return;

        hpText.text = "" + hostPlayer.GetComponent<PlayerStatus>().MaxHP;
        atkText.text = "" + hostPlayer.GetComponent<PlayerStatus>().ATK;
        speedText.text = "" + hostPlayer.GetComponent <PlayerStatus>().SPEED;

        restPointText.text = "残り" + basicStatusManager.basicRestPoint + "ポイント";
    }
}
