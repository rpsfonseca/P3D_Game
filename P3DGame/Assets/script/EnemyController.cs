using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{

	public float health = 100.0f;
	public float range = 15f;
	public float turnSpeed = 10f;

	public Transform target;
	public Transform partToRotate;

	private Rigidbody rigidBody;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
		
	// Update is called once per frame
	void Update ()
	{
		Rotate ();
	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject gameObj = collision.gameObject;
		DiskController disk = gameObj.GetComponent<DiskController> ();

		if (disk != null)
		{
			TakeDamage (disk.damage);
		}
	}

	// Take damage from a particular disk
	public void TakeDamage(float amount)
	{
		health -= amount;

		//TODO: Change smoke particle system according to health
		if (health <= 0)
		{
			//FIXME: Set turret to destroyed state
			Destroy(gameObject);
		}
	}

	// Rotate enemy if player is insde range
	void Rotate()
	{
		float distanceToPlayer = Vector3.Distance (transform.position, target.position);

		if (distanceToPlayer <= range)
		{
			Vector3 direction = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation (direction);
			Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			partToRotate.rotation = Quaternion.Euler (rotation);
		}
	}
		
	// Draw range guidlines
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}


}
