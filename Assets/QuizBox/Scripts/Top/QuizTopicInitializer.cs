using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizTopicInitializer : MonoBehaviour {

	public GameObject topCellPrefab;
	public UIGrid grid;
	public UIScrollView scrollView;
	public GameObject incentiveButtonPrefab;

	void Start () {
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		int unlockLimit = 0;
		#if UNITY_IPHONE
		unlockLimit = 1;
		#endif
//		if (timeSpan.TotalDays >= unlockLimit) {
//			GameObject incentiveButtonObject = Instantiate (incentiveButtonPrefab) as GameObject;
//			grid.AddChild (incentiveButtonObject.transform);
//			incentiveButtonObject.transform.localScale = new Vector2 (1f,1f);
//			incentiveButtonObject.transform.localPosition = new Vector3 (0,0,0);
//		} 

		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		for (int i = 0; i < quizList.Count; i++) {
			IDictionary quiz = quizList [i];
			GameObject cellObject = Instantiate (topCellPrefab) as GameObject;
			grid.AddChild (cellObject.transform);
			cellObject.transform.localScale = new Vector2 (1f, 1f);
			int id = (int)quiz [QuizListDao.ID_FIELD];
			int orderNumber = (int)quiz [QuizListDao.ORDER_NUMBER_FIELD];
			string name = (string)quiz [QuizListDao.TITLE_FIELD];
			Debug.Log ("name " + name);
			string quizUrl = (string)quiz [QuizListDao.QUIZ_URL_FIELD];
			string boughtDate = (string)quiz [QuizListDao.BOUGHT_DATE_FIELD];
			CellTop cellTop = cellObject.GetComponent<CellTop> ();
			cellTop.id = id;
			cellTop.name = name;
			cellTop.quizUrl = quizUrl;
			cellTop.boughtDate = boughtDate;
			cellObject.transform.localPosition = new Vector3 (0, -(float)orderNumber, 0);
		}

		scrollView.ResetPosition ();
	}
}
