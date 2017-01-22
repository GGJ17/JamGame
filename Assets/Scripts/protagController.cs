using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class protagController : NoisyListenElem {

	float speed = 10f;
	float yPos = 1.27f;
	int health = 1;
	public GameObject camera;
	public GameObject echo;
	public GameObject light;
	public GameObject prey;
	Light echoLight;
	float delay;
	protected List<object[]> iconInfo;
	private rotateIcon[] ris;
	// Use this for initialization
	protected void Awake(){
		base.Awake ();
		ris = FindObjectsOfType<rotateIcon> ();
	}
	void Start () {
		Debug.Log ("test");
		echoLight = echo.GetComponent<Light>();
		delay = Time.time;
		noiseLevel = 100;
		detectLevel = 120;
		knownLevel = 200;
		stype = NoiseEnum.Ally;
		iconInfo = new List<object[]> ();
		//Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Prey"));
		//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y+90, camera.transform.rotation.z);
	}

	// Update is called once per frame
	void Update () {
		DetectSound();
		HandleInput();
		HandleIcon ();
	}
	void HandleIcon(){
		rotateIcon iq = null;
		rotateIcon ie = null;
		foreach (rotateIcon ri in ris) {
			ri.active = false;
			if (ri.name == "ImageE") {
				ie = ri;
			}else if(ri.name == "ImageQ"){
				iq = ri;
			}
		}
		if (iconInfo.Count > 0) {
			object[] onlyIcon = iconInfo [0];
			foreach (object[] ii in iconInfo) {
				if ((float)ii [1] > (float)onlyIcon [1]) {
					onlyIcon = ii;
				}
			}

			//angle,intensity,type,name
			if (ie == null || iq == null || onlyIcon == null) {
				Debug.Log ("NULLLLLLLLLLLLLLLLLLLLLLLl");
				//return;
			}
			if ((NoiseEnum)onlyIcon [2] != NoiseEnum.Unknown) {
				ie.angle = (float)onlyIcon [0];
				ie.intensity = (float)onlyIcon [1];
				ie.active = true;
			} else {
				iq.angle = (float)onlyIcon [0];
				iq.intensity = (float)onlyIcon [1];
				iq.active = true;
			}
		}

	}
	void DetectSound(){
		Dictionary<string,List<float[]>> d = CalcRay();
		List<object[]> info = filterNoisyObj(d);
		string tmpD = "";
		string tmpI = "";
		//List<object[]> canHear = new List<object[]>();
		//List<object[]> canKnow = new List<object[]>();
		List<object[]> aware = new List<object[]>();
		foreach (object[] en in info) {
			tmpD += "(s:"+en[3]+",t:"+en[2]+",i:"+en[1]+",a:"+en[0]+"),";
			Transform goTransform = this.GetComponent<Transform>();
			//Vector3 dir = (new Vector3 (x,y,z));
			Vector3 dir = 2*(float)en[1]*(Quaternion.Euler (0, (float)en[0], 0) * goTransform.forward);
			Debug.DrawRay(goTransform.position,dir, Color.magenta); 
			//angle,intensity,type,name
			NoisyListenElem.NoiseEnum ne = (NoiseEnum)en[2];
			if(ne != NoiseEnum.Ally && ne != NoiseEnum.Self && ne!=NoiseEnum.Obstacle){//interested?
				if((float)en[1]>knownLevel){
					object[] tmpOl = {en[0],en[1],en[2]};
					//canKnow.Add (tmpOl);
					aware.Add (tmpOl);
					tmpI += "(K"+en[2]+":"+en[3]+",a:"+en[0]+",i:"+en[1]+"),";
				}else if((float)en[1]>detectLevel){
					object[] tmpOl = {en[0],en[1],NoiseEnum.Unknown};
					tmpI += "(U"+en[2]+":"+en[3]+",a:"+en[0]+",i:"+en[1]+"),";
					aware.Add (tmpOl);
					//canHear.Add (tmpOl);
				}
			}
		}
		iconInfo = aware;
		Debug.Log ("D: "+tmpD);
		Debug.Log ("I: " + tmpI);
	}
	void HandleInput(){
		Rigidbody rb = this.GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
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
		transform.rotation = Quaternion.Euler (0, 0, 0);
		//camera.transform.rotation = Quaternion.Euler (00, 0, 0);
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
