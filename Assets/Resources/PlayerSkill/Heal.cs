using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Heal : MonoBehaviour
{
    GameObject getPlayer;

    PlayerStatus playerStatus;

    float buttonAtimer;
    float buttonBtimer;
    float buttonXtimer;
    float buttonYtimer;

    float changeButonTimer;

    public GameObject atkCollider;

    PlayerInput input;

    public GameObject[] effects;

    public GameObject meshObject;

    public GameObject healObject;
    public GameObject rangeHealObject;

    AudioSource source;
    public AudioClip[] clips;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (playerStatus == null) return;

        if (buttonAtimer >= 0) buttonAtimer -= Time.deltaTime;
        if (buttonBtimer >= 0) buttonBtimer -= Time.deltaTime;
        if (buttonXtimer >= 0) buttonXtimer -= Time.deltaTime;
        if (buttonYtimer >= 0) buttonYtimer -= Time.deltaTime;

        if(changeButonTimer >= 0) changeButonTimer -= Time.deltaTime;
    }

    public void OnA()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonAtimer >= 0) return;

        GameObject attack = Instantiate(atkCollider, getPlayer.transform.position + transform.forward + transform.up, Quaternion.identity, getPlayer.transform);
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = playerStatus.ATK + (playerStatus.wisdom / 2) + PlayerPrefs.GetInt("Round") * 2;
        ObF.effect = effects[0];
        ObF.instantiatePos = attack.transform;
        ObF.PlayingEffect(attack.transform);
        ObF.destroyEffextTime = 0.4f;
        attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.5f);
        source.PlayOneShot(clips[0]);
        buttonAtimer = 0.8f;
    }

    public void OnB()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonBtimer >= 0) return;

        getPlayer.GetComponent<PlayerStatus>().PlayerGetDamage(((playerStatus.ATK + playerStatus.wisdom + PlayerPrefs.GetInt("Round")) / 2) * -1);
        GameObject effect = Instantiate(effects[1], getPlayer.transform);
        effect.AddComponent<ObjectFalse>().Invoke("DestroySphere", 1.5f);
        source.PlayOneShot(clips[1]);

        buttonBtimer = 10;
        
    }

    public void OnX()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonXtimer >= 0) return;

        GameObject healItem = Instantiate(healObject, getPlayer.transform.position + getPlayer.transform.forward + transform.up, Quaternion.identity);
        healItem.GetComponent<HealObject>().SettingPoints(playerStatus.ATK + playerStatus.wisdom + PlayerPrefs.GetInt("Round"), effects[2]);

        healItem.GetComponent<Rigidbody>().AddForce(getPlayer.transform.forward * 1000);
        source.PlayOneShot(clips[2]);

        buttonXtimer = 8;
    }

    public void OnY()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonYtimer >= 0) return;

        GameObject rangeHealing = Instantiate(rangeHealObject,getPlayer.transform);
        rangeHealing.GetComponent<HealObject>().SettingPoints((playerStatus.ATK + playerStatus.wisdom + PlayerPrefs.GetInt("Round")) / 2, effects[3]);
        Instantiate(effects[3],transform.position,Quaternion.Euler(-90,0,0),rangeHealing.transform);
        rangeHealing.AddComponent<DestroyEffect>().DestroyTime(1.5f);
        source.PlayOneShot(clips[3]);

        buttonYtimer = 15;
    }

    public void OnStickButton()
    {
        if (playerStatus.HP <= 0) return;
        if (playerStatus.weapon == null) return;
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (changeButonTimer >= 0) return;

        changeButonTimer = 10;
        transform.parent = null;
        transform.position = getPlayer.transform.position + (transform.forward * -3) + transform.up;
        GetComponent<BoxCollider>().enabled = true;
        gameObject.AddComponent<Rigidbody>();
        meshObject.transform.localPosition = Vector3.zero;

        playerStatus.weapon = null;
        playerStatus = null;
        getPlayer = null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = (GameObject)collision.gameObject;
            if (player.GetComponent<PlayerStatus>().weapon != null) return;
            playerStatus = player.GetComponent<PlayerStatus>();

            //atk = playerStatus.ATK + playerStatus.shoatRange;

            playerStatus.weapon = gameObject;

            transform.parent = player.transform;
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
            GetComponent<BoxCollider>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
            Destroy(GetComponent<Rigidbody>());

            getPlayer = player;

            meshObject.transform.localPosition = new Vector3(0, 1, -0.5f);
        }
    }
}
