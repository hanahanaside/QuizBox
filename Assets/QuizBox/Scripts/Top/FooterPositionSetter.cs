using UnityEngine;
using System.Collections;

public class FooterPositionSetter : MonoBehaviour {

	public UIAnchor uiAnchor;

	// Use this for initialization
	void Start () {
#if UNITY_IPHONE
		string generationName = iPhone.generation.ToString();
		Debug.Log("generation = "+ generationName);
		if(generationName == "iPhone4S" || generationName == "iPhone4"){
			uiAnchor.relativeOffset.Set(0,0.145f);
		}
#endif

		#if UNITY_ANDROID

		#endif
	}
}
