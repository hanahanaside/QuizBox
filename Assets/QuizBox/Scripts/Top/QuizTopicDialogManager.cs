using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizTopicDialogManager : MonoBehaviour {

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
		quizList.Sort (CompareByOrderNumber);

		for (int i = 0; i < quizList.Count; i++) {
			Quiz quiz = quizList [i];
			GameObject cellObject = Instantiate (topCellPrefab) as GameObject;
			grid.AddChild (cellObject.transform);
			cellObject.transform.localScale = new Vector2 (1f, 1f);
			CellTop cellTop = cellObject.GetComponent<CellTop> ();
			cellTop.id = quiz.Id;
			cellTop.Name = quiz.Title;
			cellTop.quizUrl = quiz.QuizUrl;
			cellTop.boughtDate = quiz.BoughtDate;
			cellObject.transform.localPosition = new Vector3 (0, -i, 0);
			Debug.Log ("name " + quiz.Title);
			Debug.Log ("num " + quiz.OrderNumber);
			Debug.Log ("id" + quiz.Id);
		}
		scrollView.ResetPosition ();
	}

	void OnDisable () {
		Debug.Log ("OnDisable");
		List<Transform> childList = grid.GetChildList ();
		for (int i = 0; i < childList.Count; i++) {
			Transform child = childList [i];
			CellTop cellTop = child.GetComponent<CellTop> ();
			int id = cellTop.id;
			Debug.Log ("id " + id);
			Debug.Log ("title " + cellTop.Name);
			Quiz quiz = new Quiz ();
			quiz.Id = id;
			quiz.OrderNumber = i + 1;
			QuizListDao.instance.UpdateOrderNumber (quiz);
		}
	}

	private int CompareByOrderNumber (Quiz x, Quiz y) {
		int xOrderNumber = x.OrderNumber;
		int yOrderNumber = y.OrderNumber;
		return xOrderNumber - yOrderNumber;
	}
}
