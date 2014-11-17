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
		if (timeSpan.TotalDays >= unlockLimit) {
	//		GameObject incentiveButtonObject = Instantiate (incentiveButtonPrefab) as GameObject;
	//		grid.AddChild (incentiveButtonObject.transform);
	//		incentiveButtonObject.transform.localScale = new Vector2 (1f,1f);
		} 
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			GameObject cellObject = Instantiate (topCellPrefab) as GameObject;
			grid.AddChild (cellObject.transform);
			cellObject.transform.localScale = new Vector2 (1f, 1f);
			CellTop cellTop = cellObject.GetComponent<CellTop> ();
			cellTop.id = quiz.Id;
			cellTop.Name = quiz.Title;
			cellTop.quizUrl = quiz.QuizUrl;
			cellTop.boughtDate = quiz.BoughtDate;
		}
		scrollView.ResetPosition ();
	}
}
