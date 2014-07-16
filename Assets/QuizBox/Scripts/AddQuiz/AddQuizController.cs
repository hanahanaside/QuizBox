using UnityEngine;
using MiniJSON;
using System.Collections;

public class AddQuizController : MonoBehaviour {

	public AddQuizDialog addQuizDialogPrefab;
	public ShortPointDialog shortPointDialogPrefab;
	private AddQuiz mSelectedQuiz;
	private int mUserPoint;

	void OnEnable(){
		AddQuizButtonController.clickedEvent+= OnClickAddQuiz;
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
	}
	
	void OnDisable(){
		AddQuizButtonController.clickedEvent-= OnClickAddQuiz;
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
	}

	void Start(){
		mUserPoint = PrefsManager.instance.GetUserPoint();
	}

	void OnClickAddQuiz(AddQuiz addQuiz){
		int needPoint = addQuiz.point;
		if(mUserPoint < needPoint){
			ShortPointDialog shortPointDialog = Instantiate(shortPointDialogPrefab) as ShortPointDialog;
			shortPointDialog.Show();
		}else {
			mSelectedQuiz = addQuiz;
			AddQuizDialog addQuizDialog = Instantiate(addQuizDialogPrefab)as AddQuizDialog;
			addQuizDialog.Show(addQuiz);
		}
	}

	void alertButtonClickedEvent( string clickedButton )
	{
		Debug.Log( "alertButtonClickedEvent: " + clickedButton );
		if(clickedButton == "\u306f\u3044"){
			//add quiz
		}
		if(clickedButton == "\u8cfc\u5165"){
			//buy point
		}
	}
}
