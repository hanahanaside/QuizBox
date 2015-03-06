using UnityEngine;
using System.Collections;

public class AddQuizHeaderGrid : MonoBehaviour {

	private AddQuizHeaderButton[] mAddQuizHeaderButtonArray;

	void Awake(){
		mAddQuizHeaderButtonArray = GetComponentsInChildren<AddQuizHeaderButton> ();
	}

	public void PupularButonClicked(){
		SetActiveButton (0);
	}

	public void BoysComicButtonClicked(){
		SetActiveButton (1);
	}

	public void EtcButtonClicked(){
		SetActiveButton (2);
	}

	public void PracticalButtonClicked(){
		SetActiveButton (3);
	}

	public void IdolButtonClicked(){
		SetActiveButton (4);
	}

	private void SetActiveButton(int buttonIndex){
		foreach(AddQuizHeaderButton button in mAddQuizHeaderButtonArray){
			button.ShowFilter ();
		}
		mAddQuizHeaderButtonArray [buttonIndex].HideFilter ();
	}
}
