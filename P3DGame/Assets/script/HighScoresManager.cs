﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class HighScoresManager : MonoBehaviour {

	public GameObject entryPrefab;


	private AllScores allScores = new AllScores();

	// Display scores when high scores panel is enabled
	void OnEnable()
	{

		DisplayScores ();

	}

	// Remove scores when high scores panel is disabled
	void OnDisable()
	{
		foreach (Transform child in gameObject.transform) 
		{
			GameObject.Destroy (child.gameObject);
		}
	}
			
	public void DisplayScores()
	{
		LoadScores ();
		SortByScore ();
		// Get top 10, if there are 10 scores available
		int maxIterations = Mathf.Min (allScores.scores.Count, 10);

		for(int i = 0; i < maxIterations; i++)
		{
			GameObject entry = Instantiate<GameObject> (entryPrefab);
			entry.transform.SetParent(gameObject.transform);
			entry.transform.localScale = Vector3.one;
			entry.transform.Find ("Rank").GetComponent<Text> ().text = (i + 1).ToString();
			entry.transform.Find ("Name").GetComponent<Text> ().text = allScores.scores [i].playerName;
			entry.transform.Find ("Score").GetComponent<Text> ().text = allScores.scores [i].playerScore.ToString();
		}
	}

	public void SortByScore()
	{
			allScores.scores.Sort ((p1,p2)=> p2.playerScore.CompareTo(p1.playerScore));	
	}


	public void AddScore(PlayerScore ps)
	{
		// Add to scores list
		allScores.scores.Add (ps);
		string dataToJson = JsonUtility.ToJson (allScores);
		File.WriteAllText(Application.persistentDataPath + "/scores.json", dataToJson);
	}
		
	public void LoadScores()
	{
		// Read the json from the file into a string
		string dataAsJson = File.ReadAllText(Application.persistentDataPath + "/scores.json");
		// Pass the json to JsonUtility, and tell it to create a GameData object from it
		allScores = JsonUtility.FromJson<AllScores>(dataAsJson);
	}
}
