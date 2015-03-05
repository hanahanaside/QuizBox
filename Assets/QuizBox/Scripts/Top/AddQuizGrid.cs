using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddQuizGrid : MonoBehaviour {

	public GameObject addQuizButtonPrefab;

	private UIGrid mGrid;

	void Awake(){
		mGrid = GetComponent<UIGrid> ();
	}
		
	public void Show(List<SelledProject> categoryList){
		if (mGrid.GetChildList ().Count <= 0) {
			CreateGrid (categoryList.Count);
		}
		gameObject.SetActive (true);
		mGrid.Reposition ();
		List<Transform> childList = mGrid.GetChildList ();
		foreach(Transform child in childList){
			AddQuizButtonController controller = child.GetComponent<AddQuizButtonController> ();
			controller.Init (categoryList);
		}
	}

	public void Hide(){
		gameObject.SetActive (false);
	}

	public void RemoveButton(SelledProject selledProject){
		List<Transform> childList = mGrid.GetChildList ();
		foreach(Transform child in childList){
			AddQuizButtonController controller = child.GetComponent<AddQuizButtonController> ();
			string title = controller.GetTitle ();
			if(title == selledProject.title){
				Destroy (child.gameObject);
				break;
			}
		}
		mGrid.repositionNow = true;
	}

	private void CreateGrid (int count) {
		for (int i = 0; i < count; i++) {
			GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
			mGrid.AddChild (addQuizButtonObject.transform);
			addQuizButtonObject.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
}
