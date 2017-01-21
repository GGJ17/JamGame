using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytraceSource : MonoBehaviour {
	//this game object's Transform  
	private Transform goTransform;

	//the number of reflections  
	//public int nReflections = 2;
	public float rayDist = 100f;
	public int rayRes = 8;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void Awake () {
		goTransform = this.GetComponent<Transform>();
	}
	protected void CalcRay(){
		ArrayList al1, al2;
		rayRes =  Mathf.Clamp(rayRes,1,rayRes);

		for (int i = 0; i < rayRes; i++) {
			float yRot = (i * 360) / (rayRes);
			Vector3 dir = Quaternion.Euler (0, yRot, 0) * goTransform.forward;
			CalcRay(goTransform.position,dir,rainbowRoad(yRot),out al1,out al2);
		}

	}
	protected Color rainbowRoad(float cir360){
		float c = cir360 % 360;
		float r=0, g=0, b=0, a=1;
		if (c >= 0 && c < 120) {
			r = (120 - c)/120; g = (c - 0)/120; b = 0;
		} else if (c >= 120 && c < 240) {
			r = 0; g = (240 - c) / 120; b = (c - 120) / 120;
		} else if (c>= 240 && c < 360) {
			r = (c - 240) / 120; g = 0; b = (360 - c) / 120;
		} else {
			r = 0; g = 0; b = 0; a = 1;
		}
		return new Color (r, g, b, a);
	}
	protected void CalcRayForward(){
		ArrayList al1, al2;
		CalcRay(goTransform.position,goTransform.forward,Color.magenta,out al1,out al2);
	}
	protected void CalcRay(Vector3 pos, Vector3 direct,Color c,out ArrayList objList, out ArrayList distList){
		Ray ray;
		RaycastHit hit;
		Vector3 inDirection;

		objList =  new ArrayList ();
		distList = new ArrayList ();
		//clamp the number of reflections between 1 and int capacity  
		rayDist = Mathf.Clamp(rayDist,0,rayDist);
		ray = new Ray(pos,direct);  

		string hitObjs = "";
		float remainDist = rayDist;
		//represent the ray using a line that can only be viewed at the scene tab  
		Debug.DrawRay(pos,ray.direction * rayDist, c);  

		while(remainDist>0){  
			if (Physics.Raycast (ray.origin, ray.direction, out hit, remainDist)) {
				inDirection = Vector3.Reflect (ray.direction, hit.normal);
				ray = new Ray (hit.point, inDirection);  

				//Debug.DrawRay (hit.point, hit.normal * 3, Color.blue);  
				Debug.DrawRay (hit.point, inDirection * remainDist, c);  

				Debug.Log ("Object name: " + hit.transform.name);
				hitObjs += hit.transform.name + " " + hit.distance + ", ";
				objList.Add (hit.transform.name);
				objList.Add (hit.distance);
			} else {
				break;
			}
		}
		if(hitObjs!="")Debug.Log ("Hit: " + hitObjs);
	}
}
