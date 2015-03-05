using UnityEngine;
using System.Collections;

public class TopController : MonoSingleton<TopController> {

	public UILabel userPointLabel;

	private IContainer mCurrentContainer;

	void OnEnable(){
		AddPointDialogController.ClosedAddPointDialogEvent += ClosedAddPointDialogEvent;
	}

	void OnDisable(){
		AddPointDialogController.ClosedAddPointDialogEvent -= ClosedAddPointDialogEvent;
	}

	void Start () {
		mCurrentContainer = QuizTopicContainer.instance;
		mCurrentContainer.Show ();
		UpdateUserPointLabel ();
		ImobileManager.instance.ShowBannerAd ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void ClosedAddPointDialogEvent(){
	//	mCurrentDialogObject.SetActive (true);
	}

	public void OnTopClicked () {
		ChangeContainer (QuizTopicContainer.instance);
	}

	public void OnAddPointClicked () {
		ChangeContainer (AddPointContainer.instance);
	}

	public void OnPostQuizClicked () {
		Application.LoadLevel ("PostQuiz");
	}

	public void OnResultClicked () {
		ChangeContainer (HistoryContainer.instance);
	}

	public void OnSettingClicked () {
		ChangeContainer (SettingContainer.instance);
	}

	public void OnAddQuizClicked () {
		ChangeContainer (AddQuizContainer.instance);
	}

	public void UpdateUserPointLabel () {
		int userPoint = PrefsManager.Instance.GetUserPoint ();
		userPointLabel.text = userPoint + "pt";
	}
		
	private void ChangeContainer(IContainer container){
		mCurrentContainer.Hide ();
		mCurrentContainer = container;
		mCurrentContainer.Show ();
	}
}
