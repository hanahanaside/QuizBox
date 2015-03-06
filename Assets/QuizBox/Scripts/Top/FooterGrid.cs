using UnityEngine;
using System.Collections;

public class FooterGrid : MonoBehaviour {

	private FooterButton[] mFooterButtonArray;

	void Awake(){
		mFooterButtonArray = GetComponentsInChildren<FooterButton>();
	}

	public void TopButtonClicked(){
		ChangeActiveButton (0);
	}

	public void AddQuizButtonClicked(){
		ChangeActiveButton (1);
	}

	public void PostQuizButtonClicked(){
		ChangeActiveButton (2);
	}

	public void ResultButtonClicked(){
		ChangeActiveButton (3);
	}

	public void SettingButtonClicked(){
		ChangeActiveButton (4);
	}

	private void ChangeActiveButton(int index){
		foreach(FooterButton footerButton in mFooterButtonArray){
			footerButton.ShowFilter ();
		}
		mFooterButtonArray [index].HideFilter ();
	}
}
