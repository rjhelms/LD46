using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image DiscoPower;
    [SerializeField]
    private Text ScorePowerText;
    [SerializeField]
    private Text LevelText;
    [SerializeField]
    private Text LivesText;

    [SerializeField]
    Color goodColor;
    [SerializeField]
    Color midColor;
    [SerializeField]
    Color badColor;
    // Start is called before the first frame update
    void Start()
    {
        var PowerRectTransform = DiscoPower.transform as RectTransform;
        PowerRectTransform.sizeDelta = new Vector2(GameManager.instance.DiscoPower, PowerRectTransform.sizeDelta.y);
        if (GameManager.instance.DiscoPower > 66)
        {
            DiscoPower.color = goodColor;
        }
        else if (GameManager.instance.DiscoPower > 33)
        {
            DiscoPower.color = midColor;
        }
        else
        {
            DiscoPower.color = badColor;
        };

        ScorePowerText.text = "SCORE: " + GameManager.instance.Score.ToString();
        LevelText.text = "LEVEL " + GameManager.instance.Level.ToString();
        LivesText.text = "LIVES: " + GameManager.instance.Mans.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsRunning)
        {
            var PowerRectTransform = DiscoPower.transform as RectTransform;
            PowerRectTransform.sizeDelta = new Vector2(GameManager.instance.DiscoPower, PowerRectTransform.sizeDelta.y);
            if (GameManager.instance.DiscoPower > 66)
            {
                DiscoPower.color = goodColor;
            } else if (GameManager.instance.DiscoPower > 33)
            {
                DiscoPower.color = midColor;
            } else
            {
                DiscoPower.color = badColor;
            };

            ScorePowerText.text = "SCORE: " + GameManager.instance.Score.ToString();
            LevelText.text = "LEVEL " + GameManager.instance.Level.ToString();
            LivesText.text = "LIVES: " + GameManager.instance.Mans.ToString();
        }
    }
}
