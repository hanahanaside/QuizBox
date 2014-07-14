using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopInitializer : MonoBehaviour
{

	public GameObject topCellPrefab;
	public GameObject grid;

	void Awake ()
	{
		IList<IDictionary> quizList = QuizListDao.instance.GetQuizList ();
		foreach(IDictionary quiz in quizList){
			GameObject cellObject = Instantiate(topCellPrefab) as GameObject;
			cellObject.transform.parent = grid.transform;
			cellObject.transform.localScale = new Vector2(1f,1f);
			string id = (string)quiz[QuizListDao.ID_FIELD];
			string name = (string)quiz[QuizListDao.NAME_FIELD];
			CellTop cellTop = cellObject.GetComponent<CellTop>();
			cellTop.id = id;
			cellTop.name = name;
		}
	}
}
