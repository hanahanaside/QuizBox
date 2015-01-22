using UnityEngine;
using System.Collections;

public class ReviewAppChecker : MonoBehaviour {
	public ReviewDialog reviewDialogPrefab;
	private static int count;
	// Use this for initialization
	void Start () {
		count++;
		if (count % 5 != 0) {
			return;
		}
		if (!PrefsManager.Instance.GetReviewed ()) {
			Debug.Log ("show review dialog");
			StartCoroutine (ShowDialogCoroutine());
		}
	}

	//コルーチンにしないと落ちる
	private IEnumerator ShowDialogCoroutine(){
		yield return new WaitForSeconds(2.0f);
		ReviewDialog reviewDialog = Instantiate (reviewDialogPrefab) as ReviewDialog;
		reviewDialog.Show ();
	}
}
