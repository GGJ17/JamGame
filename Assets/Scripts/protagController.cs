﻿using System.Collections;
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
	public GameObject firefly;
	Light echoLight;
	float delay;
	private Light aoeEcho;
	private Animator animator;
	private int lastAoe=0;
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
		animator = this.GetComponent<Animator> ();
		noiseLevel = 100;
		detectLevel = 120;
		knownLevel = 200;
		stype = NoiseEnum.Ally;
		iconInfo = new List<object[]> ();
		Rigidbody rb = this.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		aoeEcho = firefly.GetComponent<Light> ();
		aoeEcho.enabled = false;
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
			rotateIcon cur;
			if ((NoiseEnum)onlyIcon [2] != NoiseEnum.Unknown) {
				cur = ie;
			} else {
				cur = iq;
				iq.angle = (float)onlyIcon [0];
				iq.intensity = (float)onlyIcon [1];
				iq.active = true;
			}
			float inten = (float)onlyIcon [1];
			float nAlpha = 0;
			if (inten >= 1.5f*knownLevel) {
				nAlpha = 1f;
			} else {
				nAlpha = (inten-detectLevel)/(1.5f*knownLevel-detectLevel);
			}
			Debug.Log ("Alpha is: "+nAlpha);
			cur.angle = (float)onlyIcon [0];
			cur.intensity = inten;
			Image im = cur.GetComponent<Image>();
			//im.color.a = nAlpha;
			Color c = im.color;
			im.color = new Color(c.r,c.g,c.b,nAlpha);
			cur.active = true;

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
		float yRot = 0;//rotDelta in deg
		float rotateSpeed = 240;
		if (Input.GetKey(KeyCode.LeftArrow)){
			// Get New Position
			Debug.Log("left");
			animator.SetBool ("isWalking", true);
			//newXPos -= (Time.deltaTime * speed);
			//yRot -= 60*Time.deltaTime;
			transform.Rotate(0, -Time.deltaTime*rotateSpeed, 0, Space.Self);
			camera.transform.Rotate(0, 0, +Time.deltaTime*rotateSpeed, Space.Self);
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			// Get New Position
			//newXPos += (Time.deltaTime * speed);
			Debug.Log("right");
			animator.SetBool ("isWalking", true);
			//yRot += 60*Time.deltaTime;
			transform.Rotate(0, +Time.deltaTime*rotateSpeed, 0, Space.Self);
			camera.transform.Rotate(0, 0, -Time.deltaTime*rotateSpeed, Space.Self);
			// Assign position
			//transform.position = new Vector3(newXPos, yPos, transform.position.z);
			//camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
			//light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);

			// Rotate Player / Camera
			//transform.rotation = Quaternion.Euler (90, transform.rotation.y+90, transform.rotation.z);
			//camera.transform.rotation = Quaternion.Euler (90, camera.transform.rotation.y+90, camera.transform.rotation.z);

		}
		Quaternion s = transform.rotation;
		Quaternion sr = Quaternion.identity;
		sr.y = s.y;
		sr.w = s.w;
		camera.transform.rotation = sr;
		camera.transform.Rotate (90, 0, 0, Space.Self);
		//Vector3 nRot = Quaternion.Euler (0, yRot, 0) * transform.forward;
		//transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y+yRot, transform.rotation.z);
		//camera.transform.rotation = Quaternion.Euler (transform.rotation.x+90, transform.rotation.y, transform.rotation.z);
		//camera.transform.localRotation = Quaternion.Euler (transform.rotation.x+90, transform.rotation.y, transform.rotation.z);
		//Quaternion.Euler (90, transform.rotation.y+90, transform.rotation.z);

		float movDelt = 0;

		if (Input.GetKeyUp("right"))
			animator.SetBool ("isWalking", false);

		if (Input.GetKey(KeyCode.DownArrow)){
			movDelt -= (Time.deltaTime * speed);
			Debug.Log("down");
			animator.SetBool ("isWalking", true);
		}

		if (Input.GetKeyUp("down")){
			animator.SetBool ("isWalking", false);

		}
		if (Input.GetKey(KeyCode.UpArrow)){
			movDelt += (Time.deltaTime * speed);
			Debug.Log("up");
			animator.SetBool ("isWalking", true);
		}
		if (Input.GetKeyUp("up"))
			animator.SetBool ("isWalking", false);
		Vector3 dpos = movDelt * transform.forward;
		dpos.y = 0;
		transform.position = transform.position + dpos;
		camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
		light.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
		firefly.transform.position = transform.position;


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

		if (Input.GetKey (KeyCode.E)) {
			if (Time.time - delay > 3f) {
				aoeEcho.enabled = true;
				delay = Time.time;
			}
		}
		if (Time.time - delay > 0.1f) {
			aoeEcho.enabled = false;
		}

		echo.transform.position = new Vector3(transform.position.x-0.05f, echo.transform.position.y, transform.position.z-2f);
		//transform.rotation = Quaternion.Euler (0, 0, 0);
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
