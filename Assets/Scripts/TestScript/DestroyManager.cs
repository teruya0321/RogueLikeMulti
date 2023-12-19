using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DestroyManager : MonoBehaviour
{
    List<Transform> players;

    public GameObject endItem;
    private void Awake()
    {
        if (GameManager.MainGameManager == null) return;

        players = GameManager.MainGameManager.players;

        Destroy(GameManager.MainGameManager.centerObj);

        Destroy(GameManager.MainGameManager.mainCamera);

        GameManager.MainGameManager.audioSource.clip = GameManager.MainGameManager.clips[2];
        GameManager.MainGameManager.audioSource.Stop();

        GameManager.MainGameManager.GetComponent<PlayerInputManager>().playerPrefab = endItem;
        SceneManager.MoveGameObjectToScene(GameManager.MainGameManager.gameObject, SceneManager.GetActiveScene());
    }

    private void Start()
    {
        foreach (Transform p in players)
        {
            Destroy(p.gameObject);
        }
    }
}
