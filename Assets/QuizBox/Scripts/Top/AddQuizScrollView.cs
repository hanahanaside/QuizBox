using UnityEngine;
using System.Collections;

public class AddQuizScrollView : MonoBehaviour {

	public PopularQuizGrid mPopularQuizGrid;

	private enum Grid {
		Popular

	}

	private Grid mCurrentGrid;

	void Awake () {
		mCurrentGrid = Grid.Popular;
		mPopularQuizGrid = GetComponentInChildren<PopularQuizGrid> ();
	}

	public void Show () {
		switch (mCurrentGrid) {
		case Grid.Popular:
			ShowPopularGrid ();
			break;
		}
	}

	public void ShowPopularGrid() {
		mPopularQuizGrid.Show ();
	}
}
