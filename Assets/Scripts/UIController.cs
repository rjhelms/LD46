using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text DiscoPowerText;

    // Start is called before the first frame update
    void Start()
    {
        DiscoPowerText.text = GameManager.instance.DiscoPower.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsRunning)
        {
            DiscoPowerText.text = GameManager.instance.DiscoPower.ToString();
        }
    }
}
