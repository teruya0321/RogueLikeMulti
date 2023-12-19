using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
    }

    private void OnDisable()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
