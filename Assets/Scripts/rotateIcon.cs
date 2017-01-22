using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateIcon : MonoBehaviour {

	public GameObject player;
	public float angle;
	public float intensity;
	private Image im;
	public bool active = true;
	private RectTransform rt;
	// Use this for initialization
	void Start () {
		//EnemyLoc
		im = this.GetComponent<Image>();
		rt = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		im.enabled = active;
		float newX = Mathf.Sin (Mathf.Deg2Rad*angle) * 80f;
		float newY = Mathf.Cos (Mathf.Deg2Rad*angle) * 80f;

		rt.localPosition = new Vector3 (newX,newY,0);
		/*
		if (Mathf.Abs (transform.position.x - newX) > 5f && Mathf.Abs (transform.position.x - newX) > 5f) {
			transform.RotateAround (new Vector3 (420.5f, 298.5f, 0f), new Vector3 (0, 0, 1), 20 * Time.deltaTime);
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		} else {
			transform.position = new Vector3 (newX, newY, transform.position.z);
		}*/
		//else if ((transform.position.x - newX) > 0.5f && (transform.position.x - newX) > 0.5f) {
		//	transform.RotateAround (new Vector3(420.5f,298.5f,0f), new Vector3(0,0,-1), 20 * Time.deltaTime);
		//	transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0);
		//}
	}
}
