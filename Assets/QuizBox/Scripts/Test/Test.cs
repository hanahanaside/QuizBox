using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Test : MonoBehaviour {

	void Start(){
		Test2 test2 = GetComponentInChildren<Test2> ();
		test2.Show ();
	}
		
}
