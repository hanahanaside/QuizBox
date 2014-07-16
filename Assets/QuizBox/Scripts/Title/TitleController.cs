using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour
{

	public GameObject seriesDialogPrefab;
	public GameObject uiRoot;

	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "QuickButton") {
			QuizListManager.instance.PlayQuickMode();
			Application.LoadLevel("Game");
		}

		if (buttonName == "SeriesButton") {
			GameObject seriesDialog = Instantiate(seriesDialogPrefab)as GameObject;
			seriesDialog.transform.parent = uiRoot.transform;
			seriesDialog.transform.localScale = new Vector3(1,1,1);
		}

		if(buttonName == "ChallengeButton"){
			QuizListManager.instance.PlayChallengeMode();
			Application.LoadLevel("Game");
		}

		if(buttonName == "BackButton"){
			Application.LoadLevel("Top");
		}
	}
}
