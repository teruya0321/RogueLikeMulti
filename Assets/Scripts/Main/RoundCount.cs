using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCount : MonoBehaviour
{
    public int round;

    // Start is called before the first frame update
    void Start()
    {
        round++;
    }

    public void RoundProgress()
    {
        PlayerPrefs.SetInt("Round",round);

        Debug.Log("�X�e�[�W" + PlayerPrefs.GetInt("Round"));

        string getStageNumber = round.ToString("00000");
        if (getStageNumber[getStageNumber.Length - 1] == '0' || getStageNumber[getStageNumber.Length - 1] == '5')
        {
            Debug.Log("�{�X��Ȃ񂾂��H");
        }

        round++;
    }

    public int ReturnCount()
    {
        return round;
    }
}
