using UnityEngine;
using System.Collections;
using System.Text;

public class TitleInitializer : MonoBehaviour {

	public GameObject titleDialogManager;
	public GameObject[] inVisibleObjectsArray;
	public UISprite backButtonSprite;
	public UILabel titleLabel;
	public UILabel quizInfoLabel;

	void Start () {
		foreach (GameObject item in inVisibleObjectsArray) {
			item.SetActive (false);
		}
		backButtonSprite.enabled = false;
		QuizListManager.instance.InitQuizList ();
	}

	public void OnLoadFinished (bool success) {
		if (success) {
			foreach (GameObject item in inVisibleObjectsArray) {
				item.SetActive (true);
			}
			backButtonSprite.enabled = true;
			titleLabel.text = SelectedQuiz.instance.name;
			StringBuilder sb = new StringBuilder();
			sb.Append("\u554f\u984c\u6570\n");
			sb.Append(QuizListManager.instance.allQuizListCount + "\u554f\n");
			sb.Append("\u8cfc\u5165\u65e5\n");
			sb.Append(SelectedQuiz.instance.boughtDate);
			quizInfoLabel.text = sb.ToString();
		} else {
			titleDialogManager.GetComponent<TitleDialogManager> ().ShowErrorDialog ();
		}
	}
}
