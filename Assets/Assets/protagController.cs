using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class protagController : MonoBehaviour {

	float speed = 10f;
	float yPos = 1.24f;

	//bool isBallPlaying = false;

	// Use this for initialization
	void Start () {
		Debug.Log ("test");
		// Get initial position of ball
		//ball.transform.localPosition = new Vector3(0, 0, 1.75f);

		// Parent to paddle
		//ball.transform.parent = this.transform;
	}

	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	void HandleInput(){
		if (Input.GetKey(KeyCode.LeftArrow)){
			// Get New Position
			float newXPos = transform.position.x - (Time.deltaTime * speed);
			Debug.Log("left");

			// Check if Min

			// Assign position
			transform.position = new Vector3(newXPos, yPos, transform.position.z);

		}

		if (Input.GetKey(KeyCode.RightArrow)){
			// Get New Position
			float newXPos = transform.position.x + (Time.deltaTime * speed);
			Debug.Log("right");

			// Check if Max

			// Assign position
			transform.position = new Vector3(newXPos, yPos, transform.position.z);

		}

		if (Input.GetKey(KeyCode.DownArrow)){
			// Get New Position
			float newZPos = transform.position.z - (Time.deltaTime * speed);
			Debug.Log("down");

			// Check if Min

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);

		}

		if (Input.GetKey(KeyCode.UpArrow)){
			// Get New Position
			float newZPos = transform.position.z + (Time.deltaTime * speed);
			Debug.Log("up");

			// Check if Max

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);

		}
			
	}
}
