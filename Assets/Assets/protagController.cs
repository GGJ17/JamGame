using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class protagController : MonoBehaviour {

	float speed = 10f;
	float yPos = 1.24f;
	int health = 1;
	public GameObject camera;
	public GameObject light;
	public GameObject prey;

	//bool isBallPlaying = false;

	// Use this for initialization
	void Start () {
		Debug.Log ("test");
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Prey"));
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

			// Assign position
			transform.position = new Vector3(newXPos, yPos, transform.position.z);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

		}

		if (Input.GetKey(KeyCode.RightArrow)){
			// Get New Position
			float newXPos = transform.position.x + (Time.deltaTime * speed);
			Debug.Log("right");
			
			// Assign position
			transform.position = new Vector3(newXPos, yPos, transform.position.z);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

		}

		if (Input.GetKey(KeyCode.DownArrow)){
			// Get New Position
			float newZPos = transform.position.z - (Time.deltaTime * speed);
			Debug.Log("down");

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

		}

		if (Input.GetKey(KeyCode.UpArrow)){
			// Get New Position
			float newZPos = transform.position.z + (Time.deltaTime * speed);
			Debug.Log("up");

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

		}
			
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Prey") {
			health++;
			Destroy (other.gameObject);
		}
	}
}
