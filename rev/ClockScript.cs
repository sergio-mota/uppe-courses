using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour {
	bool isPaused = false;
	float startTime;
	float timeRemaining;
	float percent;
	public Texture2D clockBG;
	public Texture2D clockFG;
	float clockFGMaxWidth;

	public Texture2D rightSide;
	public Texture2D leftSide;
	public Texture2D back;
	public Texture2D blocker;
	public Texture2D shiny;
	public Texture2D finished;

	// Use this for initialization
	void Start () {
		this.GetComponent<GUIText>().material.color = Color.black;
		startTime = 5.0f;
		clockFGMaxWidth = clockFG.width;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			DoCountDown ();
		}
	}

	void OnGUI() {
		int pieClockX = 100;
		int pieClockY = 50;
		int pieClockW = 64;
		int pieClockH = 64;
		int pieClockHalfW = pieClockW / 2;
		int pieClockHalfH = pieClockH / 2;

		float newBarWidth = (percent / 100.0f) * clockFGMaxWidth;
		int gap = 100;

		GUI.BeginGroup (new Rect(Screen.width - clockBG.width - gap, gap, clockBG.width, clockBG.height));
		GUI.DrawTexture (new Rect (0, 0, clockBG.width, clockBG.height), clockBG);
		GUI.BeginGroup (new Rect(5, 6, newBarWidth, clockFG.height));
		GUI.DrawTexture (new Rect (0, 0, clockFG.width, clockFG.height), clockFG);
		GUI.EndGroup ();
		GUI.EndGroup ();

		bool isPastHalfway = percent < 50.0f;
		Rect clockRect = new Rect (pieClockX, pieClockY, pieClockW, pieClockH);
		float rot = (percent / 100.0f) * 360.0f;
		Vector2 centerPoint = new Vector2 (pieClockX + pieClockHalfW, pieClockY + pieClockHalfH);
		Matrix4x4 startMatrix = GUI.matrix;

		GUI.DrawTexture (clockRect, back, ScaleMode.StretchToFill, true, 0);

		if (isPastHalfway) {
			GUIUtility.RotateAroundPivot (-rot - 180.0f, centerPoint);
			GUI.DrawTexture (clockRect, leftSide, ScaleMode.StretchToFill, true, 0);
			GUI.matrix = startMatrix;
			GUI.DrawTexture (clockRect, blocker, ScaleMode.StretchToFill, true, 0);
		} else {
			GUIUtility.RotateAroundPivot (-rot, centerPoint);
			GUI.DrawTexture (clockRect, rightSide, ScaleMode.StretchToFill, true, 0);
			GUI.matrix = startMatrix;
			GUI.DrawTexture (clockRect, leftSide, ScaleMode.StretchToFill, true, 0);
		}

		if (percent < 0.0f) {
			GUI.DrawTexture (clockRect, finished, ScaleMode.StretchToFill, true, 0);
		}

		GUI.DrawTexture (clockRect, shiny, ScaleMode.StretchToFill, true, 0);
	}

	void DoCountDown() {
		timeRemaining = startTime - Time.time;
		percent = timeRemaining / startTime * 100.0f;

		if (timeRemaining < 0.0f) {
			timeRemaining = 0.0f;
			isPaused = true;
			TimeIsUp ();
		}

		ShowTime ();
		Debug.Log ("Time remaining = " + timeRemaining);
	}

	void PauseClock() {
		isPaused = true;
	}

	void UnpauseClock() {
		isPaused = false;
	}

	void ShowTime() {
		int minutes = Mathf.RoundToInt(timeRemaining) / 60;
		int seconds = Mathf.RoundToInt(timeRemaining) % 60;

		string timeString = minutes.ToString () + ":" + seconds.ToString ("D2");
		this.GetComponent<GUIText> ().text = timeString;
	}

	void TimeIsUp() {
		Debug.Log ("Time is up!");
	}
}
