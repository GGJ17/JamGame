using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytraceSource : MonoBehaviour {
	//this game object's Transform  
	private Transform goTransform;

	//the number of reflections  
	//public int nReflections = 2;
	public float rayDist = 20f;
	public int rayRes = 60;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void Awake () {
		goTransform = this.GetComponent<Transform>();
	}
	protected Dictionary<string,List<float[]>> CalcRay(){
		rayRes =  Mathf.Clamp(rayRes,1,rayRes);
		Dictionary<string,List<float[]>> d = new Dictionary<string,List<float[]>> ();
		for (int i = 0; i < rayRes; i++) {
			List<ArrayList> al1;
			float yRot = (i * 360) / (rayRes);
			Vector3 dir = Quaternion.Euler (0, yRot, 0) * goTransform.forward;
			dir.y = 0;
			CalcRay(goTransform.position,dir,rainbowRoad(yRot),out al1);
			foreach(ArrayList a in al1){
				object[] b = a.ToArray ();
				string oname = (string) b [0];
				float dist = (float) b [1];
				List<float[]> pre;
				if (d.TryGetValue (oname, out pre)) {
					float[] tmp = {dist,yRot};
					pre.Add (tmp);
				} else {
					pre = new List<float[]>();
					float[] tmp = { dist, yRot };
					pre.Add (tmp);
					d[oname]=pre;
				}
			}
		}
		return d;
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
		List<ArrayList> al1;
		CalcRay(goTransform.position,goTransform.forward,Color.magenta,out al1);
	}
	protected void CalcRay(Vector3 pos, Vector3 direct,Color c,out List<ArrayList> objList){
		Ray ray;
		RaycastHit hit;
		Vector3 inDirection;

		objList =  new List<ArrayList> ();
		//clamp the number of reflections between 1 and int capacity  
		rayDist = Mathf.Clamp(rayDist,0,rayDist);
		ray = new Ray(pos,direct);  

		string hitObjs = "";
		float currDist = 0;
		//represent the ray using a line that can only be viewed at the scene tab  
		//Debug.DrawRay(pos,ray.direction * rayDist, c);  

		while(currDist<rayDist){  
			if (Physics.Raycast (ray.origin, ray.direction, out hit, rayDist-currDist)) {
				inDirection = Vector3.Reflect (ray.direction, hit.normal);
				inDirection.y = 0;
				Debug.DrawRay (ray.origin, ray.direction * hit.distance, c);  
				ray = new Ray (hit.point, inDirection);
				currDist += hit.distance;

				//Debug.DrawRay (hit.point, hit.normal * 3, Color.blue);  
				//Debug.DrawRay (hit.point, inDirection * (rayDist-currDist), c);

				//Debug.Log ("Object name: " + hit.transform.name);
				hitObjs += hit.transform.name + " " + currDist + ", ";
				ArrayList tmp = new ArrayList (2);
				tmp.Add (hit.transform.name);
				tmp.Add (currDist);
				objList.Add (tmp);
			} else {
				Debug.DrawRay (ray.origin, ray.direction * (rayDist-currDist), c);
				break;
			}
		}
		//if(objList.Count>0)Debug.Log ("Hit: " + hitObjs);
	}
}
