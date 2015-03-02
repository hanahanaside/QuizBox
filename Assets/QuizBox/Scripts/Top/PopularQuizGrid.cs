using UnityEngine;
using System.Collections;

public class PopularQuizGrid : MonoBehaviour {

	public GameObject addQuizButtonPrefab;
	private UIGrid mGrid;

	public void Show(){
		if(mGrid == null){
			mGrid = GetComponent<UIGrid> ();
			CreateGrid ();
		}
		mGrid.gameObject.SetActive (true);
		mGrid.Reposition ();
	}

	private void CreateGrid(){
		for(int i = 0;i < SelledProjectsArray.instance.Length;i++){
			SelledProject selledProject = SelledProjectsArray.instance.Get (i);
			GameObject addQuizButtonObject = Instantiate (addQuizButtonPrefab)as GameObject;
			mGrid.AddChild (addQuizButtonObject.transform);
			addQuizButtonObject.transform.localScale = new Vector3 (1, 1, 1);
			AddQuizButtonController controller = addQuizButtonObject.GetComponentInChildren<AddQuizButtonController> ();
			controller.Init (selledProject);
		}
	}
}
