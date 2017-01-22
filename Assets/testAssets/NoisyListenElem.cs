using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyListenElem : RaytraceSource {
	public enum NoiseEnum: int {
		Self = 0,
		Ally = 1,
		Obstacle = 2,
		Unknown = 3,
		Enemy = 4,
		UniqueEnemy1 = 5,
		UniqueEnemy2 = 6,
		UniqueEnemy3 = 7,
	}
	private const float deltaScale = 1.2f;
	public float rotDelta = 10;//(360*deltaScale) / (rayRes);
	public float noiseLevel = 100;
	public float detectLevel = 50;//detect noise louder than .
	public NoiseEnum stype = NoiseEnum.Enemy;
	private DeciferNoise dn;

	protected void Awake(){
		base.Awake();
		dn = Object.FindObjectOfType<DeciferNoise> ();
		if (dn == null) {
			Debug.Log ("NULLLLLLL");
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	protected List<object[]> filterNoisyObj(Dictionary<string,List<float[]>> d){
		//WARNING ASSUMING LIST IS IN ORDER OF rotation
		List<object[]> info = new List<object[]>();
		foreach(KeyValuePair<string,List<float[]>> entry in d){
			int count = 0;
			float totx = 0;
			float toty = 0;
			float[] first = null;
			float firstAng = -1;
			float lastAng = -1;
			foreach(float[] dr in entry.Value){
				if (count == 0) {
					count = 1;
					toty = Mathf.Sin (Mathf.Deg2Rad*dr [1]) * 1/dr [0];
					totx = Mathf.Cos (Mathf.Deg2Rad*dr [1]) * 1/dr [0];
					firstAng = dr[1];
				} else {
					if (circularDelta(lastAng,dr [1],rotDelta)) {
						count += 1;
						toty += Mathf.Sin (Mathf.Deg2Rad*dr [1]) * 1/dr [0];
						totx += Mathf.Cos (Mathf.Deg2Rad*dr [1]) * 1/dr [0];
					} else {
						if (first == null) {
							float[] tmp = { totx, toty, count };
							first = tmp;
						} else {
							//angle,intensity,type,name
							//Debug.Log ("--"+dr[1]+","+dr[0]+" "+totx+","+toty+" "+(xyToDeg(totx, toty))+";");
							info.Add (makeInfoEntry(totx,toty,count,entry.Key));
						}
						count = 1;
						toty = Mathf.Sin (Mathf.Deg2Rad*dr [1]) * 1/dr [0];
						totx = Mathf.Cos (Mathf.Deg2Rad*dr [1]) * 1/ dr [0];
					}
				}
				lastAng = dr [1];
			}
			if (first != null) { //merge first and last
				if (circularDelta(firstAng,xyToDeg(first[0],first[1]),rotDelta)) { //merge
					count += (int)first[2];
					toty += first[1];
					totx += first[0];
				} else { //separate
					info.Add (makeInfoEntry(first[0],first[1],(int)first[2],entry.Key));
				}
				info.Add (makeInfoEntry(totx,toty,count,entry.Key));
			} else {
				info.Add (makeInfoEntry(totx,toty,count,entry.Key));
			}
		}

		return info;
	}
	private object[] makeInfoEntry(float x, float y, int count, string name){
		float dist = (Mathf.Pow(Mathf.Pow(x,2) + Mathf.Pow(y,2),0.5f));///count
		//float inten = Mathf.Pow (dist, 2);
		float ang = xyToDeg(x,y);
		ang = (ang + 360) % 360;
		NoiseEnum ne;
		if (dn == null) {
			Debug.Log ("NULLLLLLL");
		}
		float intensity = dist*dn.detectObjStr (name, out ne);
		object[] tmp =  {ang , intensity, (int)ne, name };
		return tmp;
	}
	static private float xyToDeg(float x,float y){
		float t = Mathf.Rad2Deg * (Mathf.Atan (y/ x));
		return x >= 0 ? t : t + 180;
	}
	static private bool circularDelta(float a,float b,float d){
		//uses degrees
		a = (a+3600)%360;
		b = (b+3600)%360;
		float e,f;
		if (a > b) {
			e = a;
			f = b;
		} else {
			e = b;
			f = a;
		}
		if (e - f < d)
			return true;
		if ((360 - e) + f < d)
			return true;
		return false;
	}
}
