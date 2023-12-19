using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = "ó~ñ]Ç…ë≈ÇøèüÇ¡ÇΩêî:" + (PlayerPrefs.GetInt("Round") - 1);
    }
}
