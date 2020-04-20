using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public GameObject audioManager;
    private float accessTime;
    // Start is called before the first frame update
    void Awake()
    {
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
    }

    void Start()
    {
        accessTime = Time.time + 0.25f;
    }

    void Update()
    {
        if (Time.time > accessTime)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}
