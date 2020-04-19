using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int powerUp;

    [SerializeField]
    private int mansUp;

    [SerializeField]
    private int scoreUp;

    [SerializeField]
    private AudioClip audioClip;

    private bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.IsRunning)
        {
            if (!isCollected & collision.gameObject.tag == "Player")
            {
                AudioManager.instance.soundSource.PlayOneShot(audioClip);
                GameManager.instance.AddPower(powerUp);
                GameManager.instance.AddMans(mansUp);
                GameManager.instance.AddScore(scoreUp);
                isCollected = true;
                Destroy(gameObject);
            }
        }
    }
}
