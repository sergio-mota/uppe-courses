using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {
	class Card : System.Object {
		public bool isFaceUp = false;
		public bool isMatched = false;
		public string image;
		public int id;

		public Card(string image, int id) {
			this.image = image;
			this.id = id;
		}
	}

	static int cols = 4;
	static int rows = 4;
	static int totalCards = cols * rows;
	int matchesNeededToWin = Mathf.RoundToInt(0.5f * totalCards);
	int matchesMade = 0;
	int cardWidth = 100;
	int cardHeight = 100;
	List<Card> cards;
	Card[][] grid;
	List<Card> cardsFlipped;
	bool playerCanClick;
	bool playerHasWon = false;

	// Use this for initialization
	void Start () {
		playerCanClick = true;

		cards = new List<Card>();
		grid = new Card[rows][];
		cardsFlipped = new List<Card>();

		BuildDeck ();

		for (int i = 0; i < rows; ++i) {
			grid[i] = new Card[cols];

			for (int j = 0; j < cols; ++j) {
				int number = Random.Range (0, cards.Count);
				grid [i] [j] = cards [number];
				cards.RemoveAt (number);
			}
		}
	}

	void BuildDeck() {
		int totalRobots = 4;
		Card card;
		int id = 0;

		for (int i = 0; i < totalRobots; ++i) {
			List<string> robotParts = new List<string>{ "Head", "Arm", "Leg" };

			for (int j = 0; j < 2; ++j) {
				int number = Random.Range (0, robotParts.Count);
				string theMissingPart = robotParts [number];

				robotParts.RemoveAt (number);

				card = new Card ("robot" + (i + 1) + "Missing" + theMissingPart, id);
				cards.Add (card);

				card = new Card ("robot" + (i + 1) + theMissingPart, id);
				cards.Add (card);

				++id;
			}
		}
	}

	void BuildGrid() {
		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();

		for (int i = 0; i < rows; ++i) {
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();

			for (int j = 0; j < cols; ++j) {
				Card card = grid [i] [j];

				string image;
				if (card.isMatched) {
					image = "blank";
				} else {
					if (card.isFaceUp) {
						image = card.image;
					} else {
						image = "wrench";
					}
				}

				GUI.enabled = !card.isMatched;
				if (GUILayout.Button ((Texture2D) Resources.Load (image), GUILayout.Width (cardWidth))) {
					if (playerCanClick) {
						StartCoroutine (FlipCardFaceUp (card));
					}
					Debug.Log (card.image);
				}
				GUI.enabled = true;
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}

		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
	}

	IEnumerator FlipCardFaceUp (Card card) {
		card.isFaceUp = true;

		if (cardsFlipped.IndexOf(card) < 0) {
			cardsFlipped.Add (card);

			if (cardsFlipped.Count == 2) {
				playerCanClick = false;
				yield return new WaitForSeconds (1.0f);

				if (cardsFlipped [0].id == cardsFlipped [1].id) {
					// Match!
					cardsFlipped [0].isMatched = true;
					cardsFlipped [1].isMatched = true;

					++matchesMade;

					if (matchesMade >= matchesNeededToWin) {
						playerHasWon = true;
					}
				} else {
					cardsFlipped [0].isFaceUp = false;
					cardsFlipped [1].isFaceUp = false;
				}

				cardsFlipped = new List<Card> ();

				playerCanClick = true;
			}
		}
	}

	void BuildWinPrompt() {
		int winPromptWidth = 100;
		int winPromptHeight = 90;

		int halfPrompWidth = winPromptWidth / 2;
		int halfPrompHeight = winPromptHeight / 2;

		int halfScreenWidth = Screen.width / 2;
		int halfScreenHeight = Screen.height / 2;

		GUI.BeginGroup (new Rect (halfScreenWidth - halfPrompWidth, halfScreenHeight - halfPrompHeight, winPromptWidth, winPromptHeight));
		GUI.Box (new Rect (0, 0, winPromptWidth, winPromptHeight), "A winner is you!");
		if (GUI.Button (new Rect (10, 40, 80, 20), "Play Again")) {
			SceneManager.LoadScene ("Title");
		}

		GUI.EndGroup ();
	}

	// Update is called once per frame
	void OnGUI () {
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		BuildGrid ();

		if (playerHasWon) {
			BuildWinPrompt ();
		}

		GUILayout.EndArea ();
		// print ("Building grid!");
	}
}
