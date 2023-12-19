using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public Transform targetCamera;
    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GameManager.MainGameManager.mainCamera.transform;

        GetComponent<TextMesh>().text = transform.parent.name;
    }
}
