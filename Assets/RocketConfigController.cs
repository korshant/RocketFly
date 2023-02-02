using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        }
    }

   private void Rotate()
{
    rigidBody.freezeRotation = true;

    if (Input.GetMouseButton(0))
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = (worldPos - transform.position).normalized;
        direction.y = 1f;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.DORotateQuaternion(targetRotation, 0.5f);
    }

    rigidBody.freezeRotation = false;
}



}