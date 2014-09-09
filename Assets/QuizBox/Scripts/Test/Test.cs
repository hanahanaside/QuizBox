using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {

	private DateTime mDateTime;

	void Start()
	{
		mDateTime = DateTime.Now;
	}

	public void OnButton1(){
		DateTime now = DateTime.Now;
		switch(mDateTime.CompareTo(now)){
		case -1:
			Debug.Log("Post は now より古い");
			break;
		case 0:
			Debug.Log("Post と now は等しい");
			break;
		case 1:
			Debug.Log("Post は now より新しい");
			break;
		}
	}

	public void OnButton2(){
		TouchScreenKeyboard.Open("text2",TouchScreenKeyboardType.ASCIICapable);
	}

	public void OnButton3(){
		TouchScreenKeyboard.hideInput = true;
	}

}
