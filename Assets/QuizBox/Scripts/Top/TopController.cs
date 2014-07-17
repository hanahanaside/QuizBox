using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour {

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}


	public void OnButtonClick(){
		string buttonName = UIButton.current.name;
		Debug.Log(buttonName);
		if(buttonName == "AddQuizButton"){
			Application.LoadLevel("AddQuiz");
		}
		if(buttonName == "PostButton"){
			
		}
		if(buttonName == "ResultButton"){
			
		}
		if(buttonName == "SettingButton"){
			
		}
		if(buttonName == "TopButton"){
			
		}
	}
}
