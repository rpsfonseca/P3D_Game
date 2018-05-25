using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int score;
	public Text scoreText;

	// Use this for initialization
	void Start ()
    {
		score = 0;
	}

	// Update is called once per frame
	void Update ()
    {
		//score++;
		//scoreText.text = "Score: " + score;
	}

    public void IncrementScoreWithKill()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }
}