using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour{

	int mCount;

	public void Init(int count){
		mCount = count;
	}

	void Update(){
		Debug.Log ("count " + mCount);
	}
}
