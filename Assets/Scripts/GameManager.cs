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

    [SerializeField]
    private int punks;

    [SerializeField]
    private int dorks;

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
}
