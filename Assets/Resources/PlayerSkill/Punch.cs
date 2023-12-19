using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Punch : MonoBehaviour
{
    GameObject getPlayer;

    PlayerStatus playerStatus;

    Animator playerAnim;

    float buttonAtimer;
    float buttonBtimer;
    float buttonXtimer;
    float buttonYtimer;

    float changeButonTimer;

    public GameObject lariatCollider;

    public GameObject RotatelariatCollider;

    public GameObject punchCollider;

    PlayerInput input;

    public GameObject[] effects;

    public GameObject meshObject;

    AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(playerStatus == null && GameManager.MainGameManager.situation == GameManager.Situation.Loading) Destroy(gameObject);
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

        GameObject attack = Instantiate(punchCollider, getPlayer.transform.position + transform.forward + transform.up, Quaternion.Euler(0,0,0),getPlayer.transform);
        //attack.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = playerStatus.ATK + (playerStatus.shoatRange ) + PlayerPrefs.GetInt("Round") * 2 + Random.Range(0, 6);
        ObF.effect = effects[0];
        ObF.instantiatePos = attack.transform;
        ObF.PlayingEffect(attack.transform);
        ObF.destroyEffextTime = 1f;
        attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.4f);
        source.PlayOneShot(clips[0]);

        buttonAtimer = 0.8f;

        Debug.Log("殴ったなぁ！");
    }

    public IEnumerator OnB()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) yield break;
        if (playerStatus.HP <= 0) yield break;
        if (buttonBtimer >= 0) yield break;


        buttonBtimer = 99;
        source.PlayOneShot(clips[1]);
        for (int i = 0; i < 5; i++)
        {
            GameObject attack = Instantiate(punchCollider, getPlayer.transform.position + transform.forward + transform.up ,Quaternion.Euler(0,0,0),getPlayer.transform);
            //attack.GetComponent<Rigidbody>().velocity = transform.forward * 10;
            ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
            ObF.atk = playerStatus.ATK + (playerStatus.shoatRange) + PlayerPrefs.GetInt("Round") + Random.Range(0,6);
            if (i == 0)
            {
                ObF.effect = effects[1];
                ObF.instantiatePos = getPlayer.transform;
                ObF.PlayingEffect(getPlayer.transform);
            }
            else ObF.effect = null;
            attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.2f);
            Debug.Log("殴ったなぁ！");

            yield return new WaitForSeconds(0.2f);
        }

        buttonBtimer = 5f;

        Debug.Log("たくさん殴ったなぁ！");
    }

    public void OnX()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonXtimer >= 0) return;

        GameObject attack = Instantiate(lariatCollider, getPlayer.transform.position, Quaternion.identity);
        //attack.GetComponent<Rigidbody>().velocity = transform.forward * 30;
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = playerStatus.ATK + (playerStatus.shoatRange) + PlayerPrefs.GetInt("Round") + Random.Range(0, 6);
        ObF.effect = effects[2];
        ObF.destroyTime = 1;
        ObF.instantiatePos = transform;
        ObF.PlayingEffect(transform);
        //attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.2f);

        source.PlayOneShot(clips[2]);
        buttonXtimer = 1f;

        Debug.Log("ラリアットだと！？");
    }

    public void OnY()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (playerStatus.HP <= 0) return;
        if (buttonYtimer >= 0) return;

        playerAnim.SetTrigger("Rotation");
        GameObject attack = Instantiate(lariatCollider, getPlayer.transform.position, Quaternion.identity);
        //attack.GetComponent<Rigidbody>().velocity = transform.forward * 30;
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = playerStatus.ATK + (playerStatus.shoatRange) + PlayerPrefs.GetInt("Round") + Random.Range(0, 6);
        ObF.effect = effects[3];
        ObF.instantiatePos = transform;
        ObF.PlayingEffect(transform);
        attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.4f);

        buttonYtimer = 4f;
        source.PlayOneShot(clips[3]);

        Debug.Log("こいつ...回るぞ！？！？");
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

        playerAnim = null;
    }

    private void OnCollisionEnter(Collision collision)
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

            playerAnim = player.GetComponent<PlayerMove>().animator;

            getPlayer = player;

            meshObject.transform.localPosition = new Vector3(0, 1, -0.5f);
            //transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
