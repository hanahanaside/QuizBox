using UnityEngine;
using System.Collections;

public class ResultController : MonoBehaviour
{
	


	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}

	public void OnButtonClick ()
	{
		Reset ();
		string buttonName = UIButton.current.name;
		if (buttonName == "RetryButton") {
			Application.LoadLevel ("Game");
		}
		if (buttonName == "TopButton") {
			Application.LoadLevel ("Top");
		}
	}

	private void Reset ()
	{
		ScoreKeeper.instance.score = 0;
	}
}
