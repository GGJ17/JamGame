using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		var horizontal = Input.GetAxis ("Horizontal");

		if (horizontal > 0) {
			animator.SetBool ("Direction", true);
		} else {
			animator.SetBool ("Direction", false);
		}
	}
}
