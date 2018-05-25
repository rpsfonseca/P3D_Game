using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float playerHealth = 100.0f;

    public void DealPlayerDamage(float hitDamage)
    {
        health.UpdateHealth(hitDamage);
    }

    public void IncrementPlayerScore()
    {
        scoreManager.IncrementScoreWithKill();
    }
}
