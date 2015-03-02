using UnityEngine;
using System.Collections;

public class Test2 : MonoSingleton<Test2> {

	public void Show(){
		gameObject.SetActive (true);
	}
}
