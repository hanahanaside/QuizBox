using UnityEngine;
using System.Collections;
using System.Text;

public class TitleInitializer : MonoBehaviour {

	public GameObject titleDialogManager;
	public GameObject[] inVisibleObjectsArray;
	public UISprite backButtonSprite;
	public UILabel titleLabel;
	public UILabel quizInfoLabel;

	void OnEnable(){
		QuizListManager.instance.loadSucceedEvent += OnLoadSucceedEvent;
		QuizListManager.instance.loadFailedEvent += OnLoadFailedEvent;
	}

	void OnDisable(){
		QuizListManager.instance.loadSucceedEvent -= OnLoadSucceedEvent;
		QuizListManager.instance.loadFailedEvent -= OnLoadFailedEvent;
	}

	void Start () {
		foreach (GameObject item in inVisibleObjectsArray) {
			item.SetActive (false);
		}
		backButtonSprite.enabled = false;
		string title = "\u304a\u5f85\u3061\u304f\u3060\u3055\u3044";
		#if UNITY_IOS
		EtceteraBinding.showBezelActivityViewWithLabel(title);
		#endif

		#if UNITY_ANDROID
		string message = "\u554f\u984c\u3092\u53d6\u5f97\u3057\u3066\u3044\u307e\u3059";
		EtceteraAndroid.showProgressDialog (title, message);
		#endif
		QuizListManager.instance.InitQuizList ();
	}

	void OnLoadSucceedEvent(){
		DissmissProgressDialog ();
		foreach (GameObject item in inVisibleObjectsArray) {
			item.SetActive (true);
		}
		backButtonSprite.enabled = true;
		titleLabel.text = SelectedQuiz.instance.name;
		StringBuilder sb = new StringBuilder();
		sb.Append("\u554f\u984c\u6570\n");
		sb.Append(QuizListManager.instance.allQuizListCount + "\u554f\n");
		sb.Append("\u8cfc\u5165\u65e5\n");
		sb.Append(SelectedQuiz.instance.boughtDate);
		quizInfoLabel.text = sb.ToString();
	}

	void OnLoadFailedEvent(){
		DissmissProgressDialog ();
		NetworkErrorDialog dialog = new NetworkErrorDialog ();
		dialog.Show ();
	}

	private void DissmissProgressDialog(){
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		#endif

		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog ();
		#endif
	}

}
