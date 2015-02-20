using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	public GameObject seriesDialogPrefab;
	public GameObject uiRoot;


	void OnEnable () {
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += AlertButtonCoicked;
#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += AlertButtonCoicked;
		EtceteraAndroidManager.alertCancelledEvent += alertCancelledEvent;
		#endif
	}

	void OnDisable () {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= AlertButtonCoicked;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= AlertButtonCoicked;
		EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
		#endif
	}
		
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			QuizListManager.instance.ReleaseQuizList();
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnQuickModeClicked () {
		QuizListManager.instance.PlayQuickMode ();
		Application.LoadLevel ("Game");
	}

	public void OnSeriesModeClicked () {
		GameObject seriesDialog = Instantiate (seriesDialogPrefab)as GameObject;
		seriesDialog.transform.parent = uiRoot.transform;
		seriesDialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void OnChallengeModeClicked () {
		Debug.Log ("OnChallengeModeClicked");
		int id = SelectedQuiz.instance.id;
		IDictionary challengeQuizDictionary = QuizListDao.instance.GetChallengeDataById (id);
		//取得時にエラーが発生したらNullが返ってくる
		if(challengeQuizDictionary == null){
			StartChallengeDialog.Show ();
			return;
		}
		Debug.Log ("count = " + challengeQuizDictionary.Count);
		string jsonString = (string)challengeQuizDictionary [QuizListDao.CHALLENGE_QUIZ_DATA_FIELD];
		if (jsonString == "null" || string.IsNullOrEmpty(jsonString)) {
			StartChallengeDialog.Show ();
		} else {
			int questionCount = (int)challengeQuizDictionary [QuizListDao.CHALLENGE_QUIZ_COUNT];
			int correctCount = (int)challengeQuizDictionary [QuizListDao.CHALLENGE_QUIZ_CORRECT];
			QuizListManager.instance.PlayChallenteModeResume (jsonString, questionCount, correctCount);
			RestartChallengeDialog.Show ();
		}
	}

	public void OnBackClicked () {
		QuizListManager.instance.ReleaseQuizList();
		Application.LoadLevel ("Top");
	}
	
	private void AlertButtonCoicked (string clickedButton) {
		if (clickedButton == "\u6311\u6226\u3059\u308b") {
			//start challenge
			QuizListManager.instance.PlayChallengeMode ();
			Application.LoadLevel ("Game");
		}

		if (clickedButton == "再開する") {
			//restart challenge
			Application.LoadLevel ("Game");
		}

		if(clickedButton == "最初からやり直す"){
			RemoveChallengeDataDialog dialog = new RemoveChallengeDataDialog ();
			dialog.Show ();
		}

		if(clickedButton == RemoveChallengeDataDialog.NEGATIVE_BUTTON){
			int id = SelectedQuiz.instance.id;
			QuizListDao.instance.RemoveChallengeData(id);
			QuizListManager.instance.PlayChallengeMode ();
			Application.LoadLevel ("Game");
		}
	}

	#if UNITY_ANDROID
	void alertCancelledEvent(){
		NetworkErrorDialog dialog = new NetworkErrorDialog ();
		dialog.Show ();
	}
	#endif
}
