using UnityEngine;
using System.Collections;

public class AddQuizDialog : MonoBehaviour {

	public void Show(AddQuiz addQuiz){
		string quizTitle = addQuiz.title;
		int point = addQuiz.point;
		string title = "title";
		string message = quizTitle+" "+point;
		string positiveButton = "\u306f\u3044";
		string negativeButton = "\u3044\u3044\u3048";
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
		#endif
	}
}
