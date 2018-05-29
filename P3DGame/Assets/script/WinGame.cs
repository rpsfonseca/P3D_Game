using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour {


	public Text victoryText;
	public GameObject hud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player") 
		{
			Instantiate<Text> (victoryText, hud.transform);
			StartCoroutine (waiter ());
		}
	}

	IEnumerator waiter()
	{
		yield return new WaitForSeconds(5);
		GameManager.instance.KillPlayer ();
	}


}
