using UnityEngine;
using System.Collections;

public class PostQuizController : MonoBehaviour {

	public GameObject postSuccessDialog;
	public GameObject postQuizDialog;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Top");
		}
	}

	public void OnCloseSuccessDialogClick(){
		AdManager.Instance.HideRectangleAd ();
		postSuccessDialog.SetActive (false);
		postQuizDialog.SetActive (true);
	}
}
