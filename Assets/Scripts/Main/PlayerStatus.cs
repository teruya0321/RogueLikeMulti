using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameManager manager;
    public int MaxHP;

    public int HP;
    public int ATK;
    public float SPEED;

    public int getDamage = 0;

    public int shoatRange;
    public int longRange;
    public int healing;
    public int wisdom;

    PlayerMove playerMove;

    public GameObject weapon;

    public GameObject attackSphere;

    public GameObject attackEffect;

    public int statusPoint;

    public GameObject downEffect;

    public Slider slider;

    float buttonAtimer;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    private void Start()
    {
        manager = GameManager.MainGameManager;

        MaxHP = manager.basicStatusManager.HP;

        HP = manager.basicStatusManager.HP;
        ATK = manager.basicStatusManager.ATK;
        SPEED = manager.basicStatusManager.SPEED;

        playerMove.SettingSpeed((int)SPEED);

        slider.maxValue = MaxHP;
    }

    private void Update()
    {
        downEffect.SetActive(HP == 0);

        if (buttonAtimer >= 0) buttonAtimer -= Time.deltaTime;

        if (weapon == null) gameObject.tag = "Player";

        slider.value = HP;

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        if (HP <= 0)
        {
            HP = 0;

            tag = "NotPlayer";
        }
    }

    public void OnA()
    {
        if (HP <= 0) return;
        if (weapon != null) return;
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus) return;
        if (buttonAtimer >= 0) return;

        Debug.Log("Aƒ{ƒ^ƒ“‰Ÿ‚µ‚Ä‚é‚â‚ñ");

        GameObject attack = Instantiate(attackSphere,
            transform.position + transform.forward + transform.up, 
            Quaternion.Euler(transform.localEulerAngles),
            transform);

        //attack.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        ObjectFalse ObF = attack.GetComponent<ObjectFalse>();
        ObF.atk = ATK;
        ObF.effect = attackEffect;
        ObF.instantiatePos = attack.transform;
        ObF.destroyEffextTime = 0.5f;
        ObF.PlayingEffect(transform);

        attack.GetComponent<ObjectFalse>().Invoke("DestroySphere", 0.08f);

        buttonAtimer = 0.8f;
    }

    public void PlayerGetDamage(int damage)
    {
        if (HP <= 0) return;
        

        HP -= damage;
        getDamage += damage;
        Debug.Log("HP : " + HP);

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        if (HP <= 0)
        {
            HP = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NotPlayer" && collision.gameObject.GetComponent<PlayerStatus>().HP <= 0)
        {
            if (HP <= 0) return;

            int gethp = HP / 2;

            HP = gethp;
            collision.gameObject.GetComponent<PlayerStatus>().HP = gethp;

            collision.gameObject.tag = "Player";
        } 
    }
}
