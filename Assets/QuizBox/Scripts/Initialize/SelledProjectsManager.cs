using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelledProjectsManager : MonoSingleton<SelledProjectsManager> {

	public static event Action<bool> createdEvent;

	private const string SELLED_PROJECTS_URL = "http://quiz.ryodb.us/list/selled_projects.json";

	private List<SelledProject> mSelledProjectsArray;

	private List<SelledProject> mPopularList;
	private List<SelledProject> mBoysComicList;
	private List<SelledProject> mGirlsComicList;
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
			mSelledProjectsArray = new List<SelledProject> ();
			mSelledProjectsArray.AddRange (selledProjectArray);
			TrimExistQuiz ();
			CreatePopularList ();
			CreateBoysComicList ();
			CreateGirlsComicList ();
			CreatePraticalList ();
			CreateIdolList ();
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
		int count = mSelledProjectsArray.Count;
		for (int i = 0; i < count; i++) {
			SelledProject selledProject = mSelledProjectsArray [i];
			if (selledProject.id == quiz.QuizId) {
				mSelledProjectsArray.Remove (selledProject);
				break;

			}
		}
	
	}

	public void Remove (SelledProject selledProject) {
		Remove (mPopularList, selledProject);
		Remove (mBoysComicList, selledProject);
		Remove (mGirlsComicList, selledProject);
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
		return mSelledProjectsArray [index];
	}

	public int Length {
		get {
			if (mSelledProjectsArray == null) {
				return 0;
			}
			return mSelledProjectsArray.Count;
		}
	}

	public List<SelledProject> GetPopularList () {
		return mPopularList;
	}

	public List<SelledProject> GetBoysComicList () {
		return mBoysComicList;
	}

	public List<SelledProject> GetGirlsComicList () {
		return mGirlsComicList;
	}

	public List<SelledProject> GetPracticalList () {
		return mPracticalList;
	}

	public List<SelledProject> GetIdolList () {
		return mIdolList;
	}

	private void CreatePopularList () {
		mPopularList = new List<SelledProject> ();
		for (int i = 0; i < 20; i++) {
			mPopularList.Add (mSelledProjectsArray [i]);
		}
	}

	private void CreateBoysComicList () {
		mBoysComicList = new List<SelledProject> ();
		for (int i = 20; i < 40; i++) {
			mBoysComicList.Add (mSelledProjectsArray [i]);
		}
	}

	private void CreateGirlsComicList () {
		mGirlsComicList = new List<SelledProject> ();
		for (int i = 40; i < 60; i++) {
			mGirlsComicList.Add (mSelledProjectsArray [i]);
		}
	}

	private void CreatePraticalList () {
		mPracticalList = new List<SelledProject> ();
		for (int i = 60; i < 80; i++) {
			mPracticalList.Add (mSelledProjectsArray [i]);
		}
	}

	private void CreateIdolList () {
		mIdolList = new List<SelledProject> ();
		for (int i = 80; i < 100; i++) {
			mIdolList.Add (mSelledProjectsArray [i]);
		}
	}
}