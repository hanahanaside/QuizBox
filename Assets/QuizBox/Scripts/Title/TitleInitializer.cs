using UnityEngine;
using System.Collections;
using System.Text;

public class TitleInitializer : MonoBehaviour {

	public GameObject titleDialogManager;
	public GameObject[] inVisibleObjectsArray;
	public UILabel titleLabel;

	void Start () {
		foreach (GameObject item in inVisibleObjectsArray) {
			item.SetActive (false);
		}
		string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		#if UNITY_IOS
		EtceteraBinding.showBezelActivityViewWithLabel(title);
		#endif

#if UNITY_ANDROID
		string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
		EtceteraAndroid.showProgressDialog(title,message);
#endif
		QuizListManager.instance.InitQuizList ();
	}

	public void OnLoadFinished (bool success) {
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif

#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
#endif

		if (success) {
			foreach (GameObject item in inVisibleObjectsArray) {
				item.SetActive (true);
			}
			titleLabel.text = SelectedQuiz.instance.name;
		} else {
			titleDialogManager.GetComponent<TitleDialogManager> ().ShowErrorDialog ();
		}
	}
}
