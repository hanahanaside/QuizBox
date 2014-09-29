using UnityEngine;
using System.Collections;
using System.Text;

public class TitleDialogManager : MonoBehaviour {
	void OnEnable () {
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
#endif
		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClicked;
		#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif

	}
		
	void alertButtonClicked (string text) {
		if (text == "OK") {
			Application.LoadLevel ("Top");
		}
	}
	#if UNITY_ANDROID
	void alertCancelled(){
		Application.LoadLevel ("Top");
	}
	#endif
}