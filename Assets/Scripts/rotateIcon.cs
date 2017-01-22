using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateIcon : MonoBehaviour {

	public GameObject player;
	public float angle;
	public float intensity;
	public float threshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float newX = Mathf.Sin (angle) * 190f;
		float newY = Mathf.Cos (angle) * 190f;
		Debug.Log ("Ideal");
		Debug.Log (newX);
		Debug.Log (newY);
		Debug.Log ("Actual");
		Debug.Log (GetComponent<RectTransform>().localPosition.x);
		Debug.Log (GetComponent<RectTransform>().localPosition.y);
		if (Mathf.Abs (GetComponent<RectTransform>().localPosition.x - newX) < 10f && Mathf.Abs (GetComponent<RectTransform>().localPosition.y - newY) < 10f) {
			Debug.Log ("Stop!");
			GetComponent<RectTransform>().localPosition = new Vector3 (newX, newY, GetComponent<RectTransform>().localPosition.z);
		} else {
			transform.RotateAround (new Vector3 (420.5f, 298.5f, 0f), new Vector3 (0, 0, 1), 20 * Time.deltaTime);
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		}
		//else if ((transform.position.x - newX) > 0.5f && (transform.position.x - newX) > 0.5f) {
		//	transform.RotateAround (new Vector3(420.5f,298.5f,0f), new Vector3(0,0,-1), 20 * Time.deltaTime);
		//	transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		//}
	}
}
