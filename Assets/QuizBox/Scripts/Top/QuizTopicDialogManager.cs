using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizTopicDialogManager : MonoSingleton<QuizTopicDialogManager> {

	public GameObject topCellPrefab;
	public UIGrid grid;
	private static QuizTopicDialogManager sInstance;
	private List<CellTop> mCellTopList;
	private Vector3 mGridStartPosition;

	public override void OnInitialize(){
		List<Quiz> quizList = QuizListDao.instance.GetQuizList ();
		quizList.Sort (CompareByOrderNumber);
		mCellTopList = new List<CellTop> ();
		for (int i = 0; i < quizList.Count; i++) {
			Quiz quiz = quizList [i];
			AddChild (quiz);
		}
	}

	public void AddChild(Quiz quiz){
		GameObject cellObject = Instantiate (topCellPrefab) as GameObject;

		CellTop cellTop = cellObject.GetComponent<CellTop> ();
		cellTop.id = quiz.Id;
		cellTop.Name = quiz.Title;
		cellTop.quizUrl = quiz.QuizUrl;
		cellTop.boughtDate = quiz.BoughtDate;

		mCellTopList.Add (cellTop);

		grid.AddChild (cellObject.transform);
		cellObject.transform.localScale = new Vector2 (1f, 1f);
//		cellObject.transform.localPosition = new Vector3 (0, -i, 0);
	}

	public void Show () { 
		grid.Reposition ();
	}

	public void UpdateOrderNumber () {
		Debug.Log ("OnDisable");
		List<Transform> childList = grid.GetChildList ();
		for (int i = 0; i < mCellTopList.Count; i++) {
			Transform child = childList[i];
			CellTop item = child.GetComponent<CellTop> ();
			int id = item.id;
			Quiz quiz = new Quiz ();
			quiz.Id = id;
			quiz.OrderNumber = i + 1;
			QuizListDao.instance.UpdateOrderNumber (quiz);
		}
	}

	void Awake(){
		sInstance = this;
		mGridStartPosition = grid.transform.localPosition;
	}

	public static QuizTopicDialogManager Instance{
		get{
			return sInstance;
		}
	}

	public void StartDragging(){
		foreach(CellTop item in mCellTopList){
			item.SetDragDropEnabled (true);
		}
	}

	public void StopDragging(){
		foreach(CellTop item in mCellTopList){
			item.SetDragDropEnabled (false);
		}
		grid.transform.localPosition = mGridStartPosition;
		grid.transform.localEulerAngles = new Vector3 (0,0,0);
	}

	private int CompareByOrderNumber (Quiz x, Quiz y) {
		int xOrderNumber = x.OrderNumber;
		int yOrderNumber = y.OrderNumber;
		return xOrderNumber - yOrderNumber;
	}
}
