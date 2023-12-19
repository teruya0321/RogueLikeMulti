using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // プレイヤーの移動用のスクリプト
    public float speed = 1;

    public PlayerInput playerInput;
    PlayerStatus status;

    float x;
    float z;

    public Animator animator;

    public bool loading;

    public bool moving = false;

    public GameObject effects;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        status = GetComponent<PlayerStatus>();
    }

    public void SettingSpeed(int speedStatus)
    {
        speed = speedStatus;
    }

    void Update()
    {
        if (GameManager.MainGameManager.situation == GameManager.Situation.SettingStatus || GameManager.MainGameManager.situation == GameManager.Situation.Loading) return;
        if (status.HP <= 0) return;
        if (moving) return;

        //Debug.Log("俺は動くぞ！");

        x = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>().x;
        z = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>().y;

        var moveVec3 = new Vector3(x, 0, z);
        
        if(moveVec3 != Vector3.zero)
        {
            GetComponent<Rigidbody>().AddForce(moveVec3 * speed, ForceMode.Force);
            var look = Quaternion.LookRotation(moveVec3);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, 45f);
            Vector3 movement = new Vector3(x * status.SPEED, 0, z * status.SPEED);
        }
        

        gameObject.GetComponent<Rigidbody>().velocity = moveVec3 * speed * 1.5f;

        animator.SetBool("Walk", moveVec3 != Vector3.zero);

        effects.SetActive(animator.GetBool("Walk"));
    }
}
