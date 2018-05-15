using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Start a new game
	public void StartGame()
	{
		// Load game scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	// Quit game
	public void QuitGame()
	{
		Application.Quit ();
	}
}
