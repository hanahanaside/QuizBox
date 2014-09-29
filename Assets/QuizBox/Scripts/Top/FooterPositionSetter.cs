using UnityEngine;
using System.Collections;

public class FooterPositionSetter : MonoBehaviour {

	void Start () {
#if UNITY_IPHONE
		Debug.Log("width = " + Screen.width);
		Debug.Log("height = "+ Screen.height);
		if(Screen.height == 960){
			transform.localPosition = new Vector3(0,-400.0f,0);
		}
#endif
		#if UNITY_ANDROID
		if(Screen.height == 800){

		}
		#endif
	}
}
