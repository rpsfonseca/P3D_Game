using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 targetDirection;
    private bool isThrown = false;

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        if (isThrown)
        {
            transform.position += (targetDirection * 5.0f * Time.deltaTime);
        }
	}

    public void SetTarget(Vector3 position)
    {
        targetPos = position;
    }

    public void Throw()
    {
        isThrown = true;
        targetDirection = (targetPos - transform.position).normalized;
        transform.parent = null;
    }
}
