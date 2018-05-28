using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	public GameObject destroyedPrefab;

	void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.name.Equals ("Disk"))
		{
			Instantiate (destroyedPrefab, transform.position, transform.rotation);
			Destroy(gameObject);	
		}

	}

}
