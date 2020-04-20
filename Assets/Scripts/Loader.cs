using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject audioManager;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
        GameManager.instance.InstantiateLevel();
    }

    private void Start()
    {
        AstarPath.active.Scan();
    }
}
