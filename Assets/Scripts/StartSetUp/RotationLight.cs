using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    GameObject lightObject;
    // Start is called before the first frame update
    void Start()
    {
        lightObject = gameObject;

        lightObject.transform.localEulerAngles = new Vector3(Random.Range(0, 181), Random.Range(0, 90), 0);
    }
}
