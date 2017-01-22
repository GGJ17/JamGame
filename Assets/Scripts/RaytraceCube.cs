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
		Dictionary<string,List<float[]>> d = CalcRay();
		string tmp = "";
		foreach(KeyValuePair<string,List<float[]>> entry in d){
			tmp += entry.Key + ": ";
			foreach(float[] dr in entry.Value){
				tmp += dr[0]+"/"+dr[1]+",";
			}
			tmp+="; ";
		}
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
