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

    public GameObject bulletPrefab;
    private GameObject[] bullets;

    public GameObject spawnPoint;

    private int currentBulletIndex = 0;

    private bool hasStartedShoot = false;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();

        //target = GameManager.instance.player.transform;

        bullets = new GameObject[10];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletPrefab) as GameObject;
            bullets[i].SetActive(false);
            bullets[i].transform.position = spawnPoint.transform.position;
            bullets[i].GetComponent<BulletController>().initPos = spawnPoint.transform;
            bullets[i].GetComponent<BulletController>().lastPos = spawnPoint.transform.position;
        }
	}
		
	// Update is called once per frame
	void Update ()
	{
		Rotate ();

        if (!hasStartedShoot && target != null)
        {
            StartCoroutine(Shoot());
            hasStartedShoot = true;
        }

        if (hasStartedShoot && target == null)
        {
            StopCoroutine(Shoot());
            hasStartedShoot = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            target = null;
        }
    }

    void OnCollisionEnter(Collision collision)
	{
		GameObject gameObj = collision.gameObject;
		DiskController disk = gameObj.GetComponent<DiskController>();

		if (disk != null)
		{
			TakeDamage (disk.damage);
		}
	}

    IEnumerator Shoot()
    {
        while (target != null)
        {
            bullets[currentBulletIndex].GetComponent<BulletController>().direction = (target.position - spawnPoint.transform.position).normalized;
            bullets[currentBulletIndex].GetComponent<BulletController>().travelling = true;
            bullets[currentBulletIndex++].SetActive(true);
            if (currentBulletIndex == 10)
            {
                currentBulletIndex = 0;
            }
            yield return new WaitForSeconds(3.0f);
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
            GameManager.instance.IncrementPlayerScore();
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
