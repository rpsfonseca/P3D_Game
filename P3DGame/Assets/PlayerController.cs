﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float lookSensitivity = 3.0f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate movement velocity as a 3D vector
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 hMovement = transform.right * xMovement;
        Vector3 vMovement = transform.forward * zMovement;

        Vector3 velocity = (hMovement + vMovement).normalized * speed;

        motor.Move(velocity);

        // Calculate rotation as a 3D vector (turning around)
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0.0f, yRotation, 0.0f) * lookSensitivity;

        // Apply Rotation
        motor.Rotate(rotation);


        // Calculate camera rotation as a 3D vector (turning around)
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRotation, 0.0f, 0.0f) * lookSensitivity;

        // Apply camera rotation
        motor.RotateCamera(cameraRotation);
    }
}
