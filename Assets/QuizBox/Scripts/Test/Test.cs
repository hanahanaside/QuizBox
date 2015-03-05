using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Test : MonoSingleton<Test> {

	public Test2 test2;
	private int mCount;

	void Start(){
		test2.Init (mCount);
	}

	void Update(){
		mCount++;
	}
}
