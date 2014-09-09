using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizTopicInitializer : MonoBehaviour {

	public GameObject topCellPrefab;
	public UIGrid grid;
	public UIScrollView scrollView;
	
	void Start () {
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		foreach (IDictionary quiz in quizList) {
			GameObject cellObject = Instantiate (topCellPrefab) as GameObject;
			grid.AddChild (cellObject.transform);
			cellObject.transform.localScale = new Vector2 (1f, 1f);
			int id = (int)quiz [QuizListDao.ID_FIELD];
			string name = (string)quiz [QuizListDao.TITLE_FIELD];
			if(!OnSaleChecker.CheckOnSale() && name == "\u9280\u9b42\u30af\u30a4\u30ba"){
				//check gintama
				name = "\u91d1\u9b42\u30af\u30a4\u30ba";
			}
			string quizUrl = (string)quiz [QuizListDao.QUIZ_URL_FIELD];
			string boughtDate = (string)quiz [QuizListDao.BOUGHT_DATE_FIELD];
			CellTop cellTop = cellObject.GetComponent<CellTop> ();
			cellTop.id = id;
			cellTop.name = name;
			cellTop.quizUrl = quizUrl;
			cellTop.boughtDate = boughtDate;
		}
		scrollView.ResetPosition ();
	}
}
