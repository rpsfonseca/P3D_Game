using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public GameObject player;
    public Camera minimapCamera;
    public Canvas hud;
    public DiskController diskController;
    public HealthBar health;
    public Score scoreManager;
	public HighScoresManager highScoreManager;


    private float playerHealth = 100.0f;

    public void DealPlayerDamage(float hitDamage)
    {
        health.UpdateHealth(hitDamage);
    }

    public void IncrementPlayerScore()
    {
        scoreManager.IncrementScoreWithKill();
    }

	public void KillPlayer()
	{
		string playerName = PlayerPrefs.GetString ("CurrentPlayer");
		int score = scoreManager.GetScore ();

		PlayerScore playerScore = new PlayerScore ();
		playerScore.playerName = playerName;
		playerScore.playerScore = score;

		// Add score to all scores json files

		highScoreManager.LoadScores();
		highScoreManager.AddScore (playerScore);

		// Set current score
		PlayerPrefs.SetInt ("CurrentScore", score);

		// Go to end menu
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
}
