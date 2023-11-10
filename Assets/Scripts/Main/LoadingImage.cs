using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    public Image image;

    public Sprite[] loadingImages;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SettingImage");
    }
    
    public IEnumerator SettingImage()
    {
        image.sprite = loadingImages[Random.Range(0, loadingImages.Length)];

        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }
}
