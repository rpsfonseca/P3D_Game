using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DiskController : MonoBehaviour
{
	public float speed = 5.0f;
	public float damage = 10.0f;
	public float catchRadius = 0.5f;
	public float maxThrowDistance = 30.0f;

	public Camera fpsCamera;
	public GameObject player;

	private Vector3 targetDirection;
	private Vector3 localPosition;
	private Quaternion localRotation;

	private bool isThrown = false;
	private bool isCatchable = false;

	private Rigidbody rigidBody;


	void Start()
	{
		// Get relative transfomr of disk to camera
		localPosition = transform.localPosition;
		localRotation = transform.localRotation;

		// Set the rigidboy to avoid collisions with player
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.isKinematic = true;
		rigidBody.detectCollisions = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		float distanceToPlayer = Vector3.Distance (transform.position, fpsCamera.transform.position);

		if (distanceToPlayer > catchRadius) 
			isCatchable = true;
	}
		

	void Update()
	{
		// Rotate the disk around the y axis
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90.0f);


		// Check if thrown disk is in range with player
		if (isThrown && isCatchable) {
			float distanceToPlayer = Vector3.Distance (transform.position, fpsCamera.transform.position);

			if (distanceToPlayer < catchRadius || distanceToPlayer > maxThrowDistance) {
				ResetDisk ();
			}
		}

		// TODO: RETURN DISK BY PRESSING FIRE WHILE FLYING
		if (Input.GetButtonDown ("Fire1")) 
		{
			if (!isThrown) {
				Shoot ();
			} else {
				isCatchable = true;
			}
		}
			
	}

	void Shoot()
	{
		isThrown = true;
		// Remove the parent from disk and set the rigidboy to listern to collisions
		gameObject.transform.parent = null;
		rigidBody.isKinematic = false;
		rigidBody.detectCollisions = true;
		rigidBody.AddTorque (3000.0f, 400.0f, 0.0f);
		rigidBody.AddForce (fpsCamera.transform.forward * speed, ForceMode.Impulse);
	}

	void ResetDisk()
	{
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.detectCollisions = false;
		rigidBody.isKinematic = true;

		gameObject.transform.parent = fpsCamera.transform;

		transform.position = fpsCamera.transform.position;
		transform.rotation = fpsCamera.transform.rotation;
		transform.localPosition = localPosition;
		transform.localRotation = localRotation;

		isThrown = false;
		isCatchable = false;
	}

	// Draw range guidlines
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (fpsCamera.transform.position, catchRadius);
	}


}

