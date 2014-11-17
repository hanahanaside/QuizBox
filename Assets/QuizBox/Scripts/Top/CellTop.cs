using UnityEngine;
using System.Collections;

public class CellTop : MonoBehaviour
{

	private int mId;
	private string mName;
	private string mQuizUrl;
	private string mBoughtDate;

	public void OnClick(){
		Debug.Log ("click");
		SelectedQuiz.instance.id = mId;
		SelectedQuiz.instance.Name = mName;
		SelectedQuiz.instance.quizUrl = mQuizUrl;
		SelectedQuiz.instance.boughtDate = mBoughtDate;
		Application.LoadLevel("Title");
	}

	public int id {
		get {
			return mId;
		}
		set{
			mId = value;
		}
	}

	public string name {
		get {
			return mName;
		}
		set {
			mName = value;
			gameObject.GetComponentInChildren<UILabel>().text = mName;
		}
	}

	public string quizUrl{
		get{
			return mQuizUrl;
		}
		set{
			mQuizUrl = value;
		}
	}

	public string boughtDate{
		get{
			return mBoughtDate;
		}
		set{
			mBoughtDate = value;
		}
	}
}
