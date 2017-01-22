﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

	public GameObject protag;
	public GameObject Astar;
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;

	public float tgtX;
	public float tgtZ;
	private bool idle;
	private float speed = 2f;
	private Vector3 speedRot = Vector3.right * 50f;
	private Vector3 target;
	private float relapse;

	// Use this for initialization
	void Start () {
		tgtX = ((xMax - xMin) / 2) + xMin;
		tgtZ = ((zMax - zMin) / 2) + zMin;
		target = new Vector3(tgtX,transform.position.y,tgtZ);
		idle = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (idle && Mathf.Abs (target.x - transform.position.x) < 0.01f && Mathf.Abs (target.z - transform.position.z) < 0.01f) {
			tgtX = Random.Range (xMin, xMax);
			tgtZ = Random.Range (zMin, zMax);
			target = new Vector3 (tgtX, transform.position.y, tgtZ);
		} else if (idle) {
			transform.Rotate (speedRot * Time.deltaTime);
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		} else {
			// chase player
			transform.Rotate (speedRot * Time.deltaTime);
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		}
		Pathfinding pathfinder = Astar.GetComponent<Pathfinding> ();
		List<Node> path = pathfinder.GetPath ();
		if (Mathf.Abs (protag.transform.position.x - transform.position.x) < 2f && Mathf.Abs (protag.transform.position.z - transform.position.z) < 2f) {
			idle = false;
			tgtX = path [0].worldPosition.x;
			tgtZ = path [0].worldPosition.z;
			target = new Vector3 (tgtX, transform.position.y, tgtZ);
			relapse = Time.time;
		} else if (!idle && (Time.time - relapse) > 2f) {
			idle = true;
			tgtX = Random.Range (xMin, xMax);
			tgtZ = Random.Range (zMin, zMax);
			target = new Vector3 (tgtX, transform.position.y, tgtZ);
		} else if (!idle) {
			tgtX = path [0].worldPosition.x;
			tgtZ = path [0].worldPosition.z;
			target = new Vector3 (tgtX, transform.position.y, tgtZ);
		}
		transform.rotation = Quaternion.Euler (90, transform.rotation.y, transform.rotation.z);
		Debug.Log ("X!");
		Debug.Log (path [0].worldPosition.x);
		Debug.Log ("Z!");
		Debug.Log (path [0].worldPosition.z);
	}
}