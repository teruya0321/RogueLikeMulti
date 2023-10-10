using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // プレイヤーの移動用のスクリプト
    public float speed = 1;
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed;

        float z = Input.GetAxisRaw("Vertical") * speed;

        Vector3 movement = new Vector3(x, -2, z);
        /*if(myRb.velocity.magnitude < maxSpeed)
        {
            myRb.AddForce(movement);
        }*/
        gameObject.GetComponent<Rigidbody>().velocity = movement;

        if (x != 0 || z != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, toRotation, 0.15f);
            GetComponent<Rigidbody>().MoveRotation(rotation);
        }
    }
}
