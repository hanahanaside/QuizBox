using UnityEngine;
using System.Collections;

public class PostQuizController : MonoBehaviour {

	public GameObject postSuccessDialog;
	public GameObject postQuizDialog;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}

	public void OnCloseSuccessDialogClick(){
		ImobileManager.Instance.HideRectangleAd ();
		postSuccessDialog.SetActive (false);
		postQuizDialog.SetActive (true);
	}
}
