using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour
{

	public GameObject seriesDialogPrefab;
	public GameObject uiRoot;

	void OnEnable ()
	{
#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += AlertButtonCoicked;
#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += AlertButtonCoicked;
		#endif
	}

	void OnDisable ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= AlertButtonCoicked;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= AlertButtonCoicked;
		#endif
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}
	
	public void OnQuickModeClicked(){
		QuizListManager.instance.PlayQuickMode ();
		Application.LoadLevel ("Game");
	}

	public void OnSeriesModeClicked(){
		GameObject seriesDialog = Instantiate (seriesDialogPrefab)as GameObject;
		seriesDialog.transform.parent = uiRoot.transform;
		seriesDialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void OnChallengeModeClicked(){
		int id = SelectedQuiz.instance.id;
		IDictionary challengeQuizDictionary = QuizListDao.instance.GetChallengeData (id);
		string jsonString = (string)challengeQuizDictionary [QuizListDao.CHALLENGE_QUIZ_DATA_FIELD];
		if (jsonString == "null") {
			StartChallengeDialog.Show ();
		} else {
			int questionCount = (int)challengeQuizDictionary[QuizListDao.CHALLENGE_QUIZ_COUNT];
			int correctCount = (int)challengeQuizDictionary[QuizListDao.CHALLENGE_QUIZ_CORRECT];
			QuizListManager.instance.PlayChallenteModeResume(jsonString,questionCount,correctCount);
			RestartChallengeDialog.Show();
		}

	}

	public void OnBackClicked(){
		Application.LoadLevel ("Top");
	}
	
	private void AlertButtonCoicked (string clickedButton)
	{
		if (clickedButton == "\u6311\u6226\u3059\u308b") {
			//start challenge
			QuizListManager.instance.PlayChallengeMode ();
			Application.LoadLevel ("Game");
		}

		if(clickedButton == "\u518d\u958b\u3059\u308b"){
			//restart challenge
			Application.LoadLevel ("Game");
		}
	}
}
