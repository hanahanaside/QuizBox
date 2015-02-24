using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizTopicDialogManager : MonoBehaviour {

	public GameObject topCellPrefab;
	public UIGrid grid;
	public UIScrollView scrollView;
	public GameObject incentiveButtonPrefab;
	private static QuizTopicDialogManager sInstance;
	private List<CellTop> mChildList;
	private iTweenEvent mShakeEvent;
	private Vector3 mGridStartPosition;

	void OnEnable () { 
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
			Debug.Log ("order number " + quiz.OrderNumber);
			Debug.Log ("id" + quiz.Id);
			Debug.Log ("name " + quiz.Title);
		}
		scrollView.ResetPosition ();
		List<Transform> childList = grid.GetChildList ();
		mChildList = new List<CellTop> ();
		foreach(Transform child in childList){
			CellTop cellTop = child.GetComponent<CellTop> ();
			mChildList.Add (cellTop);
		}
	}

	void OnDisable () {
		Debug.Log ("OnDisable");
		List<Transform> childList = grid.GetChildList ();
		for (int i = 0; i < mChildList.Count; i++) {
			Transform child = childList[i];
			CellTop item = child.GetComponent<CellTop> ();
			int id = item.id;
			Debug.Log ("id " + id);
			Debug.Log ("title " + item.Name);
			Debug.Log ("order number " + (i + 1));
			Quiz quiz = new Quiz ();
			quiz.Id = id;
			quiz.OrderNumber = i + 1;
			QuizListDao.instance.UpdateOrderNumber (quiz);
			Destroy (child.gameObject);
		}
	}

	void Start(){
		sInstance = this;
		mShakeEvent = iTweenEvent.GetEvent (grid.gameObject,"ShakeEvent");
		mGridStartPosition = grid.transform.localPosition;
		DateTime dtNow = DateTime.Now;
		string installedDate = PrefsManager.Instance.InstalledDate;
		DateTime dtInstalled = DateTime.Parse (installedDate);
		TimeSpan timeSpan = dtNow - dtInstalled;
		if (timeSpan.TotalMinutes < 1) {
			//show
			scrollView.gameObject.transform.localPosition = new Vector3 (0,700,0);
			Debug.Log ("move");
		}


	}

	public static QuizTopicDialogManager Instance{
		get{
			return sInstance;
		}
	}

	public void StartDragging(){
		foreach(CellTop item in mChildList){
			item.SetDragDropEnabled (true);
		}
		mShakeEvent.Play ();
	}

	public void StopDragging(){
		foreach(CellTop item in mChildList){
			item.SetDragDropEnabled (false);
		}
		mShakeEvent.Stop ();
		grid.transform.localPosition = mGridStartPosition;
		grid.transform.localEulerAngles = new Vector3 (0,0,0);
	}

	private int CompareByOrderNumber (Quiz x, Quiz y) {
		int xOrderNumber = x.OrderNumber;
		int yOrderNumber = y.OrderNumber;
		return xOrderNumber - yOrderNumber;
	}
}
