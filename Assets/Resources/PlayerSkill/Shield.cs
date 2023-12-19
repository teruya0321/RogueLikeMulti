using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
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

    public GameObject atkCollider;

    public GameObject shield;

    public GameObject barrier;

    public int dashTime;

    bool change = true;

    AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerStatus == null) return;

        if(buttonAtimer >= 0) buttonAtimer -= Time.deltaTime;
        if(buttonBtimer >= 0) buttonBtimer -= Time.deltaTime;
        if(buttonXtimer >= 0) buttonXtimer -= Time.deltaTime;
        if(buttonYtimer >= 0) buttonYtimer -= Time.deltaTime;

        if (changeButonTimer >= 0) changeButonTimer -= Time.deltaTime;
    }

    public void OnA()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonAtimer >= 0) return;
        if (changeButonTimer >= 0) return;

        //changeButonTimer = 10;

        GameObject attack = Instantiate(atkCollider, getPlayer.transform.position + transform.forward + transform.up, Quaternion.identity, getPlayer.transform);
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = playerStatus.ATK + (playerStatus.healing / 2) + PlayerPrefs.GetInt("Round") * 2;
        ObF.effect = effects[0];
        ObF.instantiatePos = attack.transform;
        ObF.PlayingEffect(attack.transform);
        ObF.destroyEffextTime = 0.4f;
        attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.5f);
        source.PlayOneShot(clips[0]);

        buttonAtimer = 0.8f;
    }

    public IEnumerator OnB()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) yield break;
        if (playerStatus.HP <= 0) yield break;
        if (buttonBtimer >= 0) yield break;

        change = false;
        buttonBtimer = 99;
        shield.SetActive(true);
        GameObject effect = Instantiate(effects[1], getPlayer.transform.position + (transform.forward * -1), Quaternion.Euler(getPlayer.transform.localEulerAngles),getPlayer.transform);
        source.PlayOneShot(clips[1]);

        yield return new WaitForSeconds(4f);

        shield.SetActive(false);
        Destroy(effect);

        change = true;
        buttonBtimer = 4;
    }

    public IEnumerator OnX()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) yield break;
        if (playerStatus.HP <= 0) yield break;
        if (buttonXtimer >= 0) yield break;

        change = false;
        buttonXtimer = 99;
        source.PlayOneShot(clips[2]);
        GameObject effect = Instantiate(effects[2], getPlayer.transform.position, Quaternion.Euler(getPlayer.transform.localEulerAngles),getPlayer.transform);
        meshObject.transform.localPosition = new Vector3(0, 1, 0.5f);
        shield.SetActive(true);
        getPlayer.GetComponent<PlayerMove>().moving = true;
        for (int i = 0; i < dashTime; i++)
        {
            getPlayer.GetComponent<Rigidbody>().velocity += getPlayer.transform.forward * 3;

            yield return new WaitForSeconds(0.1f);
        }

        getPlayer.GetComponent<PlayerMove>().moving = false;
        meshObject.transform.localPosition = new Vector3(0, 1, -0.5f);

        shield.SetActive(false);

        Destroy(effect);

        change = true;
        buttonXtimer = 4;
    }

    public IEnumerator OnY()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) yield break;
        if (playerStatus.HP <= 0) yield break;

        if (buttonYtimer >= 0) yield break;

        change = false;
        buttonYtimer = 99;
        barrier.SetActive(true);
        getPlayer.tag = "NotPlayer";
        GameObject effect = Instantiate(effects[3], transform.position, Quaternion.Euler(-90,0,0),transform);
        source.PlayOneShot(clips[3]);

        yield return new WaitForSeconds(6f);

        barrier.SetActive(false);
        Destroy (effect);
        getPlayer.tag = "Player";

        change = true;
        buttonYtimer = 4;
    }

    public void OnStickButton()
    {
        if (playerStatus.HP <= 0) return;
        if (playerStatus.weapon == null) return;
        if (!change) return;
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        //if (changeButonTimer <= 0) return;

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
            //Destroy(GetComponent<BoxCollider>());

            getPlayer = player;

            meshObject.transform.localPosition = new Vector3(0, 1, -0.5f);
        }
    }
}
