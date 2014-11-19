using UnityEngine;
using System.Collections;

public class CellTop : MonoBehaviour {

	private int mId;
	private string mName;
	private string mQuizUrl;
	private string mBoughtDate;
	private float mTime;
	private bool mIsDown;
	private UIDragDropItem mDragDropItem;

	public void OnClick () {
		Debug.Log ("click");
		SelectedQuiz.instance.id = mId;
		SelectedQuiz.instance.Name = mName;
		SelectedQuiz.instance.quizUrl = mQuizUrl;
		SelectedQuiz.instance.boughtDate = mBoughtDate;
		Application.LoadLevel ("Title");
	}

	void Start(){
		mDragDropItem = GetComponent<UIDragDropItem> ();
	}

	void OnPress (bool isDown) {
		mIsDown = isDown;
		mTime = 1.0f;
		if(!isDown){
			QuizTopicDialogManager.Instance.StopDragging ();
			transform.localEulerAngles = new Vector3 (0,0,0);
		}
	}
		
	void Update () {
		if (!mIsDown) {
			return;
		}
		mTime -= Time.deltaTime;
		if (mTime > 0) {
			return;
		}
		if(!mDragDropItem.enabled){
			QuizTopicDialogManager.Instance.StartDragging ();
		}
	}

	public void SetDragDropEnabled(bool state){
		mDragDropItem.enabled = state;
	}

	public int id {
		get {
			return mId;
		}
		set {
			mId = value;
		}
	}

	public string Name {
		get {
			return mName;
		}
		set {
			mName = value;
			gameObject.GetComponentInChildren<UILabel> ().text = mName;
		}
	}

	public string quizUrl {
		get {
			return mQuizUrl;
		}
		set {
			mQuizUrl = value;
		}
	}

	public string boughtDate {
		get {
			return mBoughtDate;
		}
		set {
			mBoughtDate = value;
		}
	}
}
