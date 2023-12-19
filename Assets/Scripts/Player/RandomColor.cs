using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public GameObject model;
    public Material mat;
    public Color[] co;
    // Start is called before the first frame update
    void Start()
    {
        SetColor();
    }
    void SetColor()
    {
        co[0] = Color.white;
        co[1] = Color.red;
        co[2] = Color.green;
        co[3] = Color.blue;
        co[4] = Color.yellow;
        co[5] = Color.black;
        co[6] = Color.cyan;
        co[7] = Color.magenta;

        ChangeColor(co[Random.Range(0,co.Length)]);
    }

    void ChangeColor(Color c)
    {
        mat.color = c;
    }
}
