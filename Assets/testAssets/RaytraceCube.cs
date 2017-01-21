using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytraceCube : RaytraceSource {

	float speed = 10f;
	float rotateSpeed = 30f;
	float yPos = 0f;
	// Use this for initialization
	void Start () {
		//base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		//base.Update();
		CalcRay();
		HandleInput();
	}
	void HandleInput(){
		if (Input.GetKey(KeyCode.LeftArrow)){
			// Get New Position
			float newXPos = transform.position.x - (Time.deltaTime * speed);
			Debug.Log("left");

			// Assign position
			//transform.position = new Vector3(newXPos, yPos, transform.position.z);
			transform.Rotate(0, -Time.deltaTime*rotateSpeed, 0, Space.Self);
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			// Get New Position
			float newXPos = transform.position.x + (Time.deltaTime * speed);
			Debug.Log("right");
			// Assign position
			//transform.position = new Vector3(newXPos, yPos, transform.position.z);
			transform.Rotate(0, Time.deltaTime*rotateSpeed, 0, Space.Self);
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			// Get New Position
			float newZPos = transform.position.z - (Time.deltaTime * speed);
			Debug.Log("down");
			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
		}
		if (Input.GetKey(KeyCode.UpArrow)){
			// Get New Position
			float newZPos = transform.position.z + (Time.deltaTime * speed);
			Debug.Log("up");
			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
		}

	}
}
