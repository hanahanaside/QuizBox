using UnityEngine;
using System.Collections;

public class SeriesDialogController : MonoBehaviour {

	public GameObject seriesButtonPrefab;
	public GameObject scrollView;

	void Start(){
		IList seriesList = QuizListManager.instance.SeriesList;
		foreach(object series in seriesList){
			string seriesName = series.ToString();
			GameObject seriesButton = Instantiate(seriesButtonPrefab)as GameObject;
			seriesButton.transform.parent = scrollView.transform;
			seriesButton.transform.localScale = new Vector3(1,1,1);
			seriesButton.GetComponentInChildren<UILabel>().text = seriesName;
		}
	}

	public void OnButtonClick(){
		string buttonName = UIButton.current.name;
		if(buttonName == "BackButton"){
			Destroy(gameObject.transform.parent.gameObject);
		}
	}
}
