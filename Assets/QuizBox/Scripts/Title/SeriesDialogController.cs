using UnityEngine;
using System.Collections;

public class SeriesDialogController : MonoBehaviour {

	public GameObject seriesButtonPrefab;
	public UIScrollView scrollView;
	public UIGrid grid;

	void Start () {
		IList seriesList = QuizListManager.instance.SeriesList;
		foreach (object series in seriesList) {
			string seriesName = series.ToString ();
			GameObject seriesButton = Instantiate (seriesButtonPrefab)as GameObject;
			grid.AddChild (seriesButton.transform);
			seriesButton.transform.localScale = new Vector3 (1, 1, 1);
			seriesButton.GetComponentInChildren<UILabel> ().text = seriesName;
		}
		scrollView.ResetPosition ();
	}

	public void OnButtonClick () {
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			Destroy (gameObject.transform.parent.gameObject);
		}
	}
}
