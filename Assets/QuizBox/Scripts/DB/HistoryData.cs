﻿using UnityEngine;
using System.Collections;

public class HistoryData
{

	private int mId; 
	private int mFlagTweet;
	private double mAverage;
	private string mTitle;
	private string mMode;
	private string mDate;
	private string mResult;

	public int id {
		get {
			return mId;
		}
		set {
			mId = value;
		}
	}

	public double Average{
		get{
			return mAverage;
		}
		set{
			mAverage = value;
		}
	}

	public string title {
		get {
			return mTitle;
		}
		set {
			mTitle = value;
		}
	}

	public string mode {
		get {
			return mMode;
		}
		set {
			mMode = value;
		}
	}

	public string date {
		get {
			return mDate;
		}
		set {
			mDate = value;
		}
	}

	public string result {
		get {
			return mResult;
		}
		set {
			mResult = value;
		}
	}

	public int flagTweet{
		get{
			return mFlagTweet;
		}
		set{
			mFlagTweet = value;
		}
	}
}
