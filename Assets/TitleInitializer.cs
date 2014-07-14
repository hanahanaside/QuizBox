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
		QuizListManager.instance.InitQuizList ();
	}

	public void OnLoadFinished (bool success)
	{
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
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
