using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
