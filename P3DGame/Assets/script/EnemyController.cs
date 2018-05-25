using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{

	public float health = 100.0f;
	public float range = 15f;
	public float turnSpeed = 10f;

	private Transform target;

	private Rigidbody rigidBody;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();

        target = GameManager.instance.player.transform;
	}
		
	// Update is called once per frame
	void Update ()
	{
		Rotate ();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        target = other.transform;
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
        /*float distanceToPlayer = Vector3.Distance (transform.position, target.position);

		if (distanceToPlayer <= range)
		{
			Vector3 direction = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation (direction);
			Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			transform.rotation = Quaternion.Euler (rotation);
		}*/

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            transform.forward = direction.normalized;
        }
	}
		
	// Draw range guidlines
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}


}
