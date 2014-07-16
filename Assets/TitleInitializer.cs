using UnityEngine;
using System.Collections;
using System.Text;

public class TitleInitializer : MonoBehaviour
{

	public GameObject titleDialogManager;
	public UILabel middleLabel;

	void Start ()
	{
		#if UNITY_IOS
		EtceteraBinding.showActivityView();
		#endif

#if UNITY_ANDROID
		string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
		EtceteraAndroid.showProgressDialog(title,message);
#endif
		QuizListManager.instance.InitQuizList ();
	}

	public void OnLoadFinished (bool success)
	{
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif

#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
#endif

		if (success) {
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.Append ("最終更新日 ： ");
			stringBuilder.Append (System.Environment.NewLine + System.Environment.NewLine);
			stringBuilder.Append ("問題数 ： ");
			stringBuilder.Append (System.Environment.NewLine + System.Environment.NewLine);
			stringBuilder.Append ("挑戦者数 ： ");
			middleLabel.text = stringBuilder.ToString ();
		} else {
			#if !UNITY_EDITOR
			titleDialogManager.GetComponent<TitleDialogManager> ().ShowErrorDialog ();
			#endif

		}
	}
}
