using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateIcon : MonoBehaviour {

	public GameObject player;
	public float angle;
	public float intensity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float newX = Mathf.Sin (angle) * 190f;
		float newY = Mathf.Cos (angle) * 190f;

		if (Mathf.Abs (transform.position.x - newX) > 5f && Mathf.Abs (transform.position.x - newX) > 5f) {
			transform.RotateAround (new Vector3 (420.5f, 298.5f, 0f), new Vector3 (0, 0, 1), 20 * Time.deltaTime);
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		} else {
			transform.position = new Vector3 (newX, newY, transform.position.z);
		}
		//else if ((transform.position.x - newX) > 0.5f && (transform.position.x - newX) > 0.5f) {
		//	transform.RotateAround (new Vector3(420.5f,298.5f,0f), new Vector3(0,0,-1), 20 * Time.deltaTime);
		//	transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		//}
	}
}
