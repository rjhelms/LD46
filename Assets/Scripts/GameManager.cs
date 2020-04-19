using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private int discoPower;
    [SerializeField]
    private int mans;
    [SerializeField]
    private int score;
    
    public int DiscoPower { get => discoPower; }
    public int Mans { get => mans; }
    public int Score { get => score; }

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

    public void RemovePower(int power)
    {
        discoPower -= power;
        if (discoPower < 0)
        {
            Debug.Log("Game over");
            Debug.Break();
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
}
