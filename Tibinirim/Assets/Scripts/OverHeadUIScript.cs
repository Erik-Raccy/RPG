using UnityEngine;
using System.Collections;

public class OverHeadUIScript : MonoBehaviour {
	private Vector3 screenPosition;
	private Camera camera;
	// Use this for initialization
	void Start () {
		camera = GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame
	void OnGUI () {
		screenPosition = camera.WorldToScreenPoint(transform.position);// gets screen position.
		screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y
		Rect rect = new Rect(screenPosition.x - 50, screenPosition.y - 100, 100, 24);// makes a rect centered at the player ( 100x24 )
		GUI.Box(rect, "Bob Ross");
	}
}
