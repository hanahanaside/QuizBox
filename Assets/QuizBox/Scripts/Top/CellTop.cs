using UnityEngine;
using System.Collections;

public class CellTop : MonoBehaviour
{

	private string mId;
	private string mName;

	public void OnClick(){
		SelectedQuiz.instance.id = mId;
		SelectedQuiz.instance.name = mName;
		Application.LoadLevel("Title");
	}

	public string id {
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
}
