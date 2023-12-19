using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    public Image image;

    public Sprite[] loadingImages;

    public SettingStatusManager settingStatusManager;
    private void OnEnable()
    {
        StartCoroutine("SettingImage");
    }

    public IEnumerator SettingImage()
    {
        image.sprite = loadingImages[Random.Range(0, loadingImages.Length)];

        yield return new WaitForSeconds(3);

        if(settingStatusManager.havingPoint) GameManager.MainGameManager.situation = GameManager.Situation.SettingStatus;
        else GameManager.MainGameManager.situation = GameManager.Situation.SpawnTime;


        GameManager.MainGameManager.ReturnRoundCount().RoundProgress();

        gameObject.SetActive(false);
    }
}
