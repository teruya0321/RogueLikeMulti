using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectColor : MonoBehaviour
{
    GameObject player;

    PlayerInput playerInput;

    public Material playerMaterial;

    public float lotationSpeed;

    float x;

    public int colorNumber = 0;

    public float[] colorList = new float[3];

    TextMesh playerName;
    void Up(InputAction.CallbackContext context)
    {
        colorNumber--;
    }

    void Down(InputAction.CallbackContext context)
    {
        colorNumber++;
    }
    private void StickButton(InputAction.CallbackContext context)
    {
        player.transform.position = Vector3.up;

        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        playerName.color = playerMaterial.color;
    }

    private void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            colorList[i] = Mathf.Clamp(colorList[i],0,1);
        }
        if(player != null)
        {
#if UNITY_EDITOR
        x = Input.GetAxis("Horizontal");

#else
        x = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>().x;
#endif

            player.transform.localEulerAngles += new Vector3(0, x * lotationSpeed, 0) * Time.deltaTime;

            playerMaterial.color = new Color(colorList[0], colorList[1], colorList[2]);

            if (playerInput.currentActionMap.FindAction("A").IsPressed()) colorList[colorNumber] += 0.01f;

            if (playerInput.currentActionMap.FindAction("Y").IsPressed()) colorList[colorNumber] -= 0.01f;

            if (colorNumber > 2) colorNumber = 2;
            if(colorNumber < 0) colorNumber = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;

            player.transform.position = transform.position + Vector3.up;

            playerInput = player.GetComponent<PlayerInput>();
            playerMaterial = player.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material;

            player.GetComponent<PlayerMove>().enabled = false;
            player.GetComponent<PlayerMove>().animator.SetBool("Walk", false);

            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            playerInput.actions["X"].started += Up;
            playerInput.actions["B"].started += Down;

            playerInput.actions["StickButton"].started += StickButton;

            playerName = transform.Find("PlayerText").GetComponent<TextMesh>();

            colorList[0] = playerMaterial.color.r;
            colorList[1] = playerMaterial.color.g;
            colorList[2] = playerMaterial.color.b;
        }
    }
}
