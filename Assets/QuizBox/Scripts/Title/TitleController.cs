using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour
{

	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "QuickButton") {
			QuizListManager.instance.PlayQuickMode();
			Application.LoadLevel("Game");
		}

		if (buttonName == "SeriesButton") {

		}

		if(buttonName == "ChallengeButton"){

		}

		if(buttonName == "BackButton"){
			Application.LoadLevel("Top");
		}
	}
}
