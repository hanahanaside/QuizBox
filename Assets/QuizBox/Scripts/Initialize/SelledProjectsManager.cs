using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelledProjectsManager : MonoSingleton<SelledProjectsManager> {

	public static event Action<bool> createdEvent;

	private const string SELLED_PROJECTS_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	private List<SelledProject> mSelledProjectsList;

	private List<SelledProject> mPopularList;
	private List<SelledProject> mBoysComicList;
	private List<SelledProject> mEtcComicList;
	private List<SelledProject> mPracticalList;
	private List<SelledProject> mIdolList;

	public override void OnInitialize () {
		DontDestroyOnLoad (gameObject);
	}

	public void Create () {
		Debug.Log ("create selled projects array");
		WWWClient wwwClient = new WWWClient (this, SELLED_PROJECTS_URL);
		wwwClient.OnSuccess = (WWW www) => {
			string json = www.text;
			SelledProject[] selledProjectArray = JsonFx.Json.JsonReader.Deserialize<SelledProject[]> (json);
			mSelledProjectsList = new List<SelledProject> ();
			mSelledProjectsList.AddRange (selledProjectArray);
			TrimExistQuiz ();
			CreateBoysComicList ();
			CreateEtcComicList ();
			CreatePraticalList ();
			CreateIdolList ();
			CreatePopularList ();
			createdEvent (true);
		};
		wwwClient.OnFail = (WWW www) => {
			createdEvent (false);
		};
		wwwClient.OnTimeOut = () => {
			createdEvent (false);
		};
		wwwClient.GetData ();
	}

	private void TrimExistQuiz () {
		List<Quiz> quizList =	QuizListDao.instance.GetQuizList ();
		foreach (Quiz quiz in quizList) {
			TrimExistQuiz (quiz);
		}
	}

	private void TrimExistQuiz (Quiz quiz) {
		int count = mSelledProjectsList.Count;
		for (int i = 0; i < count; i++) {
			SelledProject selledProject = mSelledProjectsList [i];
			if (selledProject.id == quiz.QuizId) {
				mSelledProjectsList.Remove (selledProject);
				break;

			}
		}
	
	}

	public void Remove (SelledProject selledProject) {
		Remove (mPopularList, selledProject);
		Remove (mBoysComicList, selledProject);
		Remove (mEtcComicList, selledProject);
		Remove (mPracticalList, selledProject);
		Remove (mIdolList, selledProject);
	}

	private void Remove (List<SelledProject> selledProjcetList, SelledProject targetProjet) {
		foreach (SelledProject selledProject in selledProjcetList) {
			if (selledProject.title == targetProjet.title) {
				selledProjcetList.Remove (selledProject);
				break;
			}
		}
	}

	public SelledProject Get (int index) {
		return mSelledProjectsList [index];
	}

	public int Length {
		get {
			if (mSelledProjectsList == null) {
				return 0;
			}
			return mSelledProjectsList.Count;
		}
	}

	public List<SelledProject> GetPopularList () {
		return mPopularList;
	}

	public List<SelledProject> GetBoysComicList () {
		return mBoysComicList;
	}

	public List<SelledProject> GetEtcComicList () {
		return mEtcComicList;
	}

	public List<SelledProject> GetPracticalList () {
		return mPracticalList;
	}

	public List<SelledProject> GetIdolList () {
		return mIdolList;
	}

	private void CreatePopularList () {
		mPopularList = new List<SelledProject> ();
		mPopularList = mSelledProjectsList;
	}

	private void CreateBoysComicList () {
		mBoysComicList = new List<SelledProject> ();
		foreach(SelledProject selledProject in mSelledProjectsList){
			if (selledProject.title.Contains ("boy_")) {
				selledProject.title = selledProject.title.Replace ("boy_","");
				mBoysComicList.Add (selledProject);
			}
		}
	}

	private void CreateEtcComicList () {
		mEtcComicList = new List<SelledProject> ();
		foreach (SelledProject selledProject in mSelledProjectsList) {
			if (selledProject.title.Contains ("etc_")) {
				selledProject.title = selledProject.title.Replace ("etc_","");
				mEtcComicList.Add (selledProject);
			}
		}
	}

	private void CreatePraticalList () {
		mPracticalList = new List<SelledProject> ();
		foreach (SelledProject selledProject in mSelledProjectsList) {
			if (selledProject.title.Contains ("study_")) {
				selledProject.title = selledProject.title.Replace ("study_","");
				mPracticalList.Add (selledProject);
			}
		}
	}

	private void CreateIdolList () {
		mIdolList = new List<SelledProject> ();
		foreach (SelledProject selledProject in mSelledProjectsList) {
			if (selledProject.title.Contains ("idol_")) {
				selledProject.title = selledProject.title.Replace ("idol_","");
				mIdolList.Add (selledProject);
			}
		}
	}
}