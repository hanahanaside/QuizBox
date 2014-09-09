using UnityEngine;
using System.Collections;

public class FooterPositionSetter : MonoBehaviour {

	public UIAnchor uiAnchor;

	// Use this for initialization
	void Start () {
#if UNITY_IPHONE
		if(Screen.height == 960){
		uiAnchor.relativeOffset.Set(0,0.145f);
		}
#endif


		#if UNITY_ANDROID

		#endif
	}
}
