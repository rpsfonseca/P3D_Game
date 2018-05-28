using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndMenu : MonoBehaviour {

	public Text scoreText;

	public void ReturnToMainMenu(){
		SceneManager.LoadScene(0);
	}

	public void Start(){
		DisplayScore ();
	}

	public void DisplayScore()
	{
		scoreText.text = "Your score: " + PlayerPrefs.GetInt ("CurrentScore",0);
	}
}
