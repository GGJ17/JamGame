using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciferNoise : MonoBehaviour {
	private NoisyListenElem[] noisyAgents;
	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		object[] tmp = Object.FindObjectsOfType<NoisyListenElem>();
		noisyAgents = (NoisyListenElem[])tmp;
	}
	// Update is called once per frame
	void Update () {
		
	}
	public float detectObjStr(string objname, out NoisyListenElem.NoiseEnum ne){
		ne = NoisyListenElem.NoiseEnum.Obstacle;
		foreach (NoisyListenElem nle in noisyAgents) {
			if(nle.name == objname){
				ne = nle.stype;
				return nle.noiseLevel;
			}
		}
		return 0;//not noisy
	}
}
