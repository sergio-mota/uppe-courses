using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGUI : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public GUISkin customSkin;
	void OnGUI () {
		GUI.skin = customSkin;

		int buttonWidth = 100;
		int buttonHeight = 50;

		int halfButtonWidth = buttonWidth / 2;
		int halfScreenWidth = Screen.width / 2;

		if (GUI.Button (new Rect (halfScreenWidth - halfButtonWidth, 480, buttonWidth, buttonHeight), "Play Game")) {
			SceneManager.LoadScene ("Game");
		}
	}
}
