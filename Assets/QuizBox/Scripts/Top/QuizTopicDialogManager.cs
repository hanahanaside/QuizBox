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
//		if (timeSpan.TotalDays >= unlockLimit) {
//			GameObject incentiveButtonObject = Instantiate (incentiveButtonPrefab) as GameObject;
//			grid.AddChild (incentiveButtonObject.transform);
//			incentiveButtonObject.transform.localScale = new Vector2 (1f,1f);
//			incentiveButtonObject.transform.localPosition = new Vector3 (0,0,0);
//		} 

		List<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		//order number順にソート
	//	quizList.Sort (CompareByOrderNumber);

		for (int i = 0; i < quizList.Count; i++) {
			IDictionary quiz = quizList [i];
			GameObject cellObject = Instantiate (topCellPrefab) as GameObject;
			grid.AddChild (cellObject.transform);
			cellObject.transform.localScale = new Vector2 (1f, 1f);
			int id = (int)quiz [QuizListDao.ID_FIELD];
			Debug.Log ("id" + id);
//			int orderNumber = (int)quiz [QuizListDao.ORDER_NUMBER];
			string name = (string)quiz [QuizListDao.TITLE_FIELD];
			Debug.Log ("name " + name);
//			Debug.Log ("order number " + orderNumber);
			string quizUrl = (string)quiz [QuizListDao.QUIZ_URL_FIELD];
			string boughtDate = (string)quiz [QuizListDao.BOUGHT_DATE_FIELD];
			CellTop cellTop = cellObject.GetComponent<CellTop> ();
			cellTop.id = id;
			cellTop.name = name;
			cellTop.quizUrl = quizUrl;
			cellTop.boughtDate = boughtDate;
			cellObject.transform.localPosition = new Vector3 (0, -i, 0);
		}
		scrollView.ResetPosition ();
	}

	void OnDisable () {
		Debug.Log ("OnDisable");
		List<Transform> childList = grid.GetChildList ();
		Debug.Log ("count " + childList.Count);
		for (int i = 0; i < childList.Count; i++) {
			Transform child = childList[i];
			CellTop cellTop = child.GetComponent<CellTop> ();
			int id = cellTop.id;

		}
	}

	private int CompareByOrderNumber (IDictionary x, IDictionary y) {
		int xOrderNumber = (int)x [QuizListDao.ORDER_NUMBER];
		int yOrderNumber = (int)y [QuizListDao.ORDER_NUMBER];
		return xOrderNumber - yOrderNumber;
	}
}
