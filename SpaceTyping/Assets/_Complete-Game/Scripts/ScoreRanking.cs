using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRanking : MonoBehaviour {
    public Text[] scoreTexts;
	// Use this for initialization
	void Start () {
        //StartCoroutine(WaitLoadScore());
        setScore();
	}
    public void setScore()
    {
        SocketHelper sh = SocketHelper.GetInstance();
        sh.getScoreList();
        Debug.Log(PlayerPrefs.GetString("score", "empty"));
        string[] scores = PlayerPrefs.GetString("score", "empty").Split(';');
        for (int i = 0; i < scores.Length-1; i++)
        {
            scoreTexts[i].text = scores[i];
        }
    }

  

}
