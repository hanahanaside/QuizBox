using UnityEngine;
using System.Collections;

public class AddQuiz
{
	private int mPoint;
	private string mUrl;
	private string mTitle;

	public string url {
		set {
			mUrl = value;
		}
		get {
			return mUrl;
		}
	}

	public int point {
		set {
			mPoint = value;
		}
		get {
			return mPoint;
		}
	}

	public string title {
		set {
			mTitle = value;
		}
		get {
			return mTitle;
		}
	}
}
