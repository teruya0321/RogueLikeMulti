using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public void DestroyTime(float time)
    {
        Invoke("DestroyObject", time);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
