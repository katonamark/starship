using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{   
    Text scoreTextUI;

    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreTextUi();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the text ui component of this GameObject
        scoreTextUI = GetComponent<Text>();
    }

    //function to update the score text UI
    void UpdateScoreTextUi()
    {
        string scoreStr = string.Format ("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
    }

    
}
