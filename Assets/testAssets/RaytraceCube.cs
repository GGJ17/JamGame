using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytraceCube : NoisyListenElem {

	float speed = 10f;
	float rotateSpeed = 30f;
	float yPos = 0f;
	// Use this for initialization
	void Start () {
		//base.Start ();
	}
	protected void Awake(){
		base.Awake ();
		noiseLevel = 100;
		detectLevel = 50;
		stype = NoiseEnum.Ally;
	}
	// Update is called once per frame
	void Update () {
		//base.Update();
		Dictionary<string,List<float[]>> d = CalcRay();
		List<object[]> info = filterNoisyObj(d);
		string tmp = "";
		foreach (object[] en in info) {
			tmp += "(s:"+en[3]+",t:"+en[2]+",i:"+en[1]+",a:"+en[0]+"),";
			Transform goTransform = this.GetComponent<Transform>();
			float x = -Mathf.Cos (Mathf.Deg2Rad * (float)en [0]) * 30;
			float y = 0;
			float z = Mathf.Sin (Mathf.Deg2Rad * (float)en [0]) * 30;
			//Vector3 dir = (new Vector3 (x,y,z));
			Vector3 dir = 2*(float)en[1]*(Quaternion.Euler (0, (float)en[0], 0) * goTransform.forward);
			Debug.DrawRay(goTransform.position,dir, Color.magenta); 
		}
//		foreach(KeyValuePair<string,List<float[]>> entry in d){
//			tmp += entry.Key + ": ";
//			foreach(float[] dr in entry.Value){
//				tmp += dr[0]+"/"+dr[1]+",";
//			}
//			tmp+="; ";
//		}
		Debug.Log ("D: "+tmp);
		HandleInput();
	}
	void HandleInput(){
		if (Input.GetKey(KeyCode.LeftArrow)){
			Debug.Log("left");
			transform.Rotate(0, -Time.deltaTime*rotateSpeed, 0, Space.Self);
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			Debug.Log("right");
			transform.Rotate(0, Time.deltaTime*rotateSpeed, 0, Space.Self);
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			float newZPos = transform.position.z - (Time.deltaTime * speed);
			Debug.Log("down");
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
		}
		if (Input.GetKey(KeyCode.UpArrow)){
			float newZPos = transform.position.z + (Time.deltaTime * speed);
			Debug.Log("up");
			transform.position = new Vector3(transform.position.x, yPos, newZPos);
		}

	}
}
