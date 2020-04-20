using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public Text scoreText;
    private float accessTime;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "YOUR SCORE: " + GameManager.instance.Score.ToString();
        accessTime = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > accessTime)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.anyKey)
            {
                Destroy(GameManager.instance.gameObject);
                SceneManager.LoadScene("Main");
            }
        }
    }
}
