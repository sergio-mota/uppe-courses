using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	float smooth = 5.0f;
	float tilt_angle = 30.0f;
	void Update () {
		Vector3 current_position = GetComponent<Transform> ().position;

		// GetComponent<Transform> ().position = new Vector3 (2.0f, current_position.y, current_position.z);

		// GetComponent<Transform> ().position = new Vector3 (current_position.x + 0.2f, current_position.y, current_position.z);

		// Debug.Log (Input.mousePosition.x);
		// GetComponent<Transform> ().position = new Vector3 (Input.mousePosition.x, current_position.y, current_position.z);

		// Debug.Log (Input.mousePosition.x - Screen.width / 2.0f);
		// GetComponent<Transform> ().position = new Vector3 (Input.mousePosition.x - Screen.width / 2.0f, current_position.y, current_position.z);

		// Debug.Log ((Input.mousePosition.x - Screen.width / 2.0f) / (Screen.width / 2.0f));
		// GetComponent<Transform> ().position = new Vector3 ((Input.mousePosition.x - Screen.width / 2.0f) / (Screen.width / 2.0f), current_position.y, current_position.z);

		// float half_width = Screen.width / 2.0f;
		// GetComponent<Transform> ().position = new Vector3 ((Input.mousePosition.x - half_width) / half_width, current_position.y, current_position.z);

		float half_width = Screen.width / 2.0f;
		float half_height = Screen.width / 2.0f;
		GetComponent<Transform> ().position = new Vector3 ((Input.mousePosition.x - half_width) / half_width, current_position.y, (Input.mousePosition.y - half_height) / half_height);

		float tilt_around_z = Input.GetAxis ("Mouse X") * tilt_angle;
		float tilt_around_x = Input.GetAxis ("Mouse Y") * tilt_angle;
		Quaternion target = Quaternion.Euler (tilt_around_x, 0.0f, tilt_around_z);
		GetComponent<Transform> ().rotation = Quaternion.Slerp (GetComponent<Transform> ().rotation, target, Time.deltaTime * smooth);
	}
}
