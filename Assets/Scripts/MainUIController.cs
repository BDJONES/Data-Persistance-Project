using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIController : MonoBehaviour
{
    //public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI highScoreInfoText;
    //public Text currentScore;
    // Start is called before the first frame update
    void Start()
    {
        MainManager.SaveData data = MainManager.Instance.LoadHighScore();
        if (data != null)
        {
            highScoreInfoText.SetText("Best Score: " + data.name + ":" + data.score);
        }
        else
        {
            highScoreInfoText.SetText("Best Score:");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
