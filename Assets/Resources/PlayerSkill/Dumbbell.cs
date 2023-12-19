using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dumbbell : MonoBehaviour
{
    GameObject getPlayer;

    PlayerStatus playerStatus;

    float buttonAtimer;
    float buttonBtimer;
    float buttonXtimer;
    float buttonYtimer;

    float changeButonTimer;

    PlayerInput input;

    public GameObject[] effects;

    public GameObject meshObject;

    public GameObject normalDumbbellAmmo;
    public GameObject penetrationDumbbelAmmo;
    public GameObject rainCollider;

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

        if (changeButonTimer >= 0) changeButonTimer -= Time.deltaTime;
    }

    public void OnA()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonAtimer >= 0) return;

        GameObject dumbell = Instantiate(normalDumbbellAmmo, transform.position + getPlayer.transform.forward + transform.up,getPlayer.transform.rotation);
        dumbell.GetComponent<ThrowDumbbel>().SettingObject(playerStatus.ATK + playerStatus.longRange + PlayerPrefs.GetInt("Round") * 3, effects[0]);
        dumbell.GetComponent<Rigidbody>().AddForce(getPlayer.transform.forward * 1000);
        source.PlayOneShot(clips[0]);

        buttonAtimer = 2;
    }

    public void OnB()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonBtimer >= 0) return;

        GameObject dumbell = Instantiate(penetrationDumbbelAmmo, transform.position + getPlayer.transform.forward + transform.up, getPlayer.transform.rotation);
        dumbell.GetComponent<PenetrateDumbbel>().SettingObject(playerStatus.ATK + playerStatus.longRange + PlayerPrefs.GetInt("Round") * 3, effects[0]);
        dumbell.GetComponent<Rigidbody>().AddForce(getPlayer.transform.forward * 1000);

        source.PlayOneShot(clips[1]);
        buttonBtimer = 4;
    }

    public void OnX()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonXtimer >= 0) return;

        int angle = -30;
        source.PlayOneShot(clips[2]);
        for (int i = 0; i < 3; i++)
        {
            GameObject dumbell = Instantiate(normalDumbbellAmmo, 
                transform.position + getPlayer.transform.forward + transform.up + (transform.right + new Vector3((-1 + i) * 3,0,0)),
                getPlayer.transform.rotation);
            dumbell.GetComponent<ThrowDumbbel>().SettingObject(playerStatus.ATK + playerStatus.longRange + PlayerPrefs.GetInt("Round") * 3, effects[0]);
            dumbell.GetComponent<Rigidbody>().AddForce((getPlayer.transform.forward) * 1000);
            angle += 45;
        }

        buttonXtimer = 6;
    }

    public void OnY()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonYtimer >= 0) return;

        GameObject rain = Instantiate(rainCollider,transform.position + (transform.forward * 3), Quaternion.identity);
        rain.GetComponent<RainAttack>().SettingObject(playerStatus.ATK * playerStatus.longRange + PlayerPrefs.GetInt("Round"));
        Instantiate(effects[1], rain.transform);
        source.PlayOneShot(clips[3]);

        buttonYtimer = 6;
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
