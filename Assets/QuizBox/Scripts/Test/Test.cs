using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour {

	void Start(){
		DateTime dtNow = DateTime.Now;
		DateTime dtPast = DateTime.Now.AddDays (-2);
		TimeSpan ts = dtNow - dtPast;
		Debug.Log ("ts = " + ts.TotalDays);
	}

}
