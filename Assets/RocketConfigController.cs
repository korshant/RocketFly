using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketConfigController : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 100f;

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        
        rigidBody = GetComponent<Rigidbody>();		
	}
	
	// FixedUpdate is called every 0.02 sec
	void FixedUpdate () {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        float thrustThisFrame = shipThrust * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            // We scale up our force by the thrust for the frame
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        if (Input.GetMouseButton(0))
        {
            // Get the mouse click position in world space
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
            // Get the direction from the rocket to the mouse click position
            Vector3 direction = (worldPos - transform.position).normalized;
        
            // Rotate the rocket to face the direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        rigidBody.freezeRotation = false; // resume physics control
    }



}