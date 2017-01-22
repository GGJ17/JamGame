using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateIcon : MonoBehaviour {

	public GameObject player;
	public 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (new Vector3(420.5f,298.5f,0f), new Vector3(0,0,1), 20 * Time.deltaTime);
		transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
	}
}
