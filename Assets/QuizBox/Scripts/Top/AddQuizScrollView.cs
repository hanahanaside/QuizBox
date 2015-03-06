using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddQuizScrollView : MonoBehaviour {

	private AddQuizGrid mCurrentGrid;
	private UIScrollView mScrollView;
	private AddQuizGrid[] mAddQuizGridArray;

	void Awake () {
		mScrollView = GetComponent<UIScrollView> ();
		mAddQuizGridArray = GetComponentsInChildren<AddQuizGrid> ();
		mCurrentGrid = mAddQuizGridArray [0];
	}
		
	public void ShowPopularGrid() {
		ShowGrid (mAddQuizGridArray [0],SelledProjectsManager.instance.GetPopularList());
		mScrollView.ResetPosition ();
	}

	public void ShowBoysComicGrid(){
		ShowGrid (mAddQuizGridArray [1],SelledProjectsManager.instance.GetBoysComicList());
		mScrollView.ResetPosition ();
	}

	public void ShowGirlsComicGrid(){
		ShowGrid (mAddQuizGridArray [2],SelledProjectsManager.instance.GetEtcComicList());
		mScrollView.ResetPosition ();
	}

	public void ShowPraticalGrid(){
		ShowGrid (mAddQuizGridArray [3],SelledProjectsManager.instance.GetPracticalList());
		mScrollView.ResetPosition ();
	}

	public void ShowIdolGrid(){
		ShowGrid (mAddQuizGridArray [4],SelledProjectsManager.instance.GetIdolList());
		mScrollView.ResetPosition ();
	}

	public void RemoveButton(SelledProject selledProject){
		foreach(AddQuizGrid grid in mAddQuizGridArray){
			grid.RemoveButton (selledProject);
		}
	}

	private void ShowGrid(AddQuizGrid grid,List<SelledProject> selledProjectList){
		mCurrentGrid.Hide ();
		mCurrentGrid = grid;
		mCurrentGrid.Show (selledProjectList);
	}
}
