using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyController : NoisyListenElem {

	// Use this for initialization
	void Start () {
		noiseLevel = 50;
		detectLevel = 200;
		knownLevel = 400;
		stype = NoiseEnum.Prey;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
