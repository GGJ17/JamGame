using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class protagController : MonoBehaviour {

	float speed = 10f;
	float yPos = 2f;
	int health = 1;
	public GameObject camera;
	public GameObject echo;
	public GameObject light;
	public GameObject prey;
	Light echoLight;
	float delay;

	// Use this for initialization
	void Start () {
		Debug.Log ("test");
		echoLight = echo.GetComponent<Light>();
		delay = Time.time;
		//Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Prey"));
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

			// Rotate Player / Camera
			//transform.rotation = Quaternion.Euler (90, transform.rotation.y-90, transform.rotation.z);
			//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y-90, camera.transform.rotation.z);

		}

		if (Input.GetKey(KeyCode.RightArrow)){
			// Get New Position
			float newXPos = transform.position.x + (Time.deltaTime * speed);
			Debug.Log("right");
			
			// Assign position
			transform.position = new Vector3(newXPos, yPos, transform.position.z);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

			// Rotate Player / Camera
			//transform.rotation = Quaternion.Euler (90, transform.rotation.y+90, transform.rotation.z);
			//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y+90, camera.transform.rotation.z);

		}

		if (Input.GetKey(KeyCode.DownArrow)){
			// Get New Position
			float newZPos = transform.position.z - (Time.deltaTime * speed);
			Debug.Log("down");

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

			// Rotate Player / Camera
			//transform.rotation = Quaternion.Euler (90, transform.rotation.y+180, transform.rotation.z);
			//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y+180, camera.transform.rotation.z);

		}

		if (Input.GetKey(KeyCode.UpArrow)){
			// Get New Position
			float newZPos = transform.position.z + (Time.deltaTime * speed);
			Debug.Log("up");

			// Assign position
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
			camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);


			// Rotate Player / Camera
			//transform.rotation = Quaternion.Euler (90, transform.rotation.y, transform.rotation.z);
			//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y, camera.transform.rotation.z);
		}

		if (Input.GetKey(KeyCode.W)){
			
			if ((Time.time - delay) > 0.25f) {
				Debug.Log(Time.time);
				delay = Time.time;
				echo.transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
				echoLight.enabled = !echoLight.enabled;
			}
		}

		if (Input.GetKey(KeyCode.D)){

			if ((Time.time - delay) > 0.25f) {
				Debug.Log(Time.time);
				delay = Time.time;
				echo.transform.rotation = Quaternion.Euler (transform.rotation.x, 90, transform.rotation.z);
				echoLight.enabled = !echoLight.enabled;
			}
		}

		if (Input.GetKey(KeyCode.S)){

			if ((Time.time - delay) > 0.25f) {
				Debug.Log(Time.time);
				delay = Time.time;
				echo.transform.rotation = Quaternion.Euler (transform.rotation.x, 180, transform.rotation.z);
				echoLight.enabled = !echoLight.enabled;
			}
		}

		if (Input.GetKey(KeyCode.A)){

			if ((Time.time - delay) > 0.25f) {
				Debug.Log(Time.time);
				delay = Time.time;
				echo.transform.rotation = Quaternion.Euler (transform.rotation.x, 270, transform.rotation.z);
				echoLight.enabled = !echoLight.enabled;
			}
		}

		echo.transform.position = new Vector3(transform.position.x-0.05f, echo.transform.position.y, transform.position.z-2f);
		transform.rotation = Quaternion.Euler (90, 90, 0);
			
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
		if (other.gameObject.tag == "Prey") {
			Debug.Log("hit");
			health++;
			Destroy (other.gameObject);
		}
	}
}
