using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    public GameObject[] levels;
    [SerializeField]
    private int discoPower;
    [SerializeField]
    private int mans;
    [SerializeField]
    private int score;

    [SerializeField]
    private int punks;
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int dorks;

    [SerializeField]
    private bool isRunning;

    public int DiscoPower { get => discoPower; }
    public int Mans { get => mans; }
    public int Score { get => score; }
    public int Punks { get => punks; }
    public int Dorks { get => dorks; }
    public int Level { get => level; }
    public bool IsRunning { get => isRunning; }

    private bool isWinning;
    private bool isLosing;
    private bool isStarting;
    private bool isEnding;

    private float fadeTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void InstantiateLevel()
    {
        Instantiate(levels[level - 1], Vector3.zero, Quaternion.identity);
        isWinning = false;
        isStarting = true;
        fadeTime = Time.time + 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isStarting)
        {
            Color coverColor = Color.Lerp(Color.clear, Color.black, fadeTime - Time.time);
            GameObject.FindGameObjectWithTag("UICover").GetComponent<Image>().color = coverColor;
            if (Time.time > fadeTime)
            {
                GameObject.FindGameObjectWithTag("UICover").GetComponent<Image>().color = Color.clear;
                isStarting = false;
                isRunning = true;
            }
        }
        if (isEnding)
        {
            Color coverColor = Color.Lerp(Color.black, Color.clear, fadeTime - Time.time);
            GameObject.FindGameObjectWithTag("UICover").GetComponent<Image>().color = coverColor;
            if (Time.time > fadeTime)
            {
                GameObject.FindGameObjectWithTag("UICover").GetComponent<Image>().color = Color.black;
                isEnding = false;
                if (isWinning)
                {
                    if (level > levels.Length)
                    {
                        Debug.Log("You win!");
                        SceneManager.LoadScene("WinScene");
                    }
                    else
                    {
                        SceneManager.LoadScene("Main");
                    }
                } else if (isLosing)
                {
                    if (mans < 0)
                    {
                        Debug.Log("You lose!");
                        SceneManager.LoadScene("LoseScene");
                    }
                    else
                    {
                        discoPower = 100;
                        SceneManager.LoadScene("Main");
                    }
                }
            }
        }
    }
    public void RemovePower(int power)
    {
        discoPower -= power;
        if (discoPower < 0)
        {
            Lose();
        }
    }

    public void AddPower(int powerUp)
    {
        discoPower += powerUp;
        if (discoPower > 100)
        {
            discoPower = 100;
        }
    }

    public void AddMans(int mansUp)
    {
        mans += mansUp;
    }

    public void AddScore(int scoreUp)
    {
        score += scoreUp;
    }

    public void RegisterPunk()
    {
        punks++;
    }

    public void RemovePunk()
    {
        if (punks > 0)
            punks--;
    }

    public void RegisterDork()
    {
        dorks++;
    }

    public void RemoveDork()
    {
        if (dorks > 0)
            dorks--;
    }

    public void Win()
    {
        if (!isWinning)
        {
            AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.levelClearSound);
            isWinning = true;
            isRunning = false;
            isEnding = true;
            level++;
            fadeTime = Time.time + 1f;

        }
    }

    public void Lose()
    {
        isRunning = false;
        isLosing = true;
        isEnding = true;
        mans--;
        AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.playerLoseSound);
        fadeTime = Time.time + 1f;
    }
}
