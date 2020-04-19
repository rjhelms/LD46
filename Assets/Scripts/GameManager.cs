using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        AstarPath.active.Scan();
        isRunning = true;
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
            level++;
            if (level > levels.Length)
            {
                Debug.Log("You win!");
            } else
            {
                SceneManager.LoadScene("Main");
            }

        }
    }

    public void Lose()
    {
        isRunning = false;
        AudioManager.instance.soundSource.PlayOneShot(AudioManager.instance.playerLoseSound);
    }
}
