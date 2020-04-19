using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioSource musicSource;
    public AudioSource soundSource;

    public AudioClip playerBoost;
    public AudioClip upgradeSound;
    public AudioClip downgradeSound;
    public AudioClip playerHitSound;
    public AudioClip danceSound;
    public AudioClip danceFizzleSound;
    public AudioClip playerThrowSound;
    public AudioClip playerLoseSound;
    public AudioClip levelClearSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            musicSource.enabled = !musicSource.enabled;
        }
    }
}
