using UnityEngine;
using System.Collections;

public class SeriesButtonController: MonoBehaviour {

	public UILabel label;

	public void OnClick(){
		string seriesName = label.text;
		Debug.Log(seriesName);
		QuizListManager.instance.PlaySeriesMode(seriesName);
		Application.LoadLevel("Game");
	}
}
