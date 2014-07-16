using UnityEngine;
using System.Collections;

public class OkDialog : MonoBehaviour {

	public void Show(string title,string message){
#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
#endif
	}
}
