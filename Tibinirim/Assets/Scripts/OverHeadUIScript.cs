using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverHeadUIScript : MonoBehaviour {
	private Vector3 screenPosition;
	//Camera camera;

	//Sliders and text for UI imported via editor
	public Slider healthSlider;
	public Text healthText;

	public Slider manaSlider;
	public Text manaText;

	public Slider stamanaSlider;
	public Text stamanaText;

	private Vector3[] pts = new Vector3[8];
	public float margin = 0;
	Bounds b;

	// Use this for initialization
	void Start () {
		//camera = GetComponentInChildren<Camera> ();
		//b = GetComponentInChildren<SkinnedMeshRenderer>().bounds;
	}

	// find player location on screen from camera and draw their name tag
	void OnGUI () {
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);// gets screen position.
		screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y
		Rect rect = new Rect(screenPosition.x - 50, screenPosition.y - 100, 100, 24);// makes a rect centered at the player ( 100x24 )
		GUI.Box(rect, "Bob Ross");
	}

	//fuctions for the player stats script to manipulate the UI
	public void setHealthSliderValue(int health) {
		healthSlider.value = health;
		healthText.text = "Health: " + health + " / " + healthSlider.maxValue;
	}
	public void setHealthSliderMaxValue(int maxHealth) {
		healthSlider.maxValue = maxHealth;
		healthText.text = "Health: " + healthSlider.value + " / " + maxHealth;
	}
		
	public void setManaSliderValue(int mana) {
		manaSlider.value = mana;
		manaText.text = "Mana: " + mana + " / " + manaSlider.maxValue;
	}
	public void setManaSliderMaxValue(int maxMana) {
		manaSlider.maxValue = maxMana;
		manaText.text = "Mana: " + manaSlider.value + " / " + maxMana;
	}
			
	public void setStamanaSliderValue(int stamana) {
		stamanaSlider.value = stamana;
		stamanaText.text = "Stamana: " + stamana + " / " + stamanaSlider.maxValue;
	}
	public void setStamanaSliderMaxValue(int maxStamana) {
		stamanaSlider.maxValue = maxStamana;
		stamanaText.text = "Stamana: " + stamanaSlider.value + " / " + maxStamana;
	}

	public void drawTargetBox (GameObject target) {
		if (target.GetComponent<MeshRenderer> () != null) {
			b = target.GetComponent<MeshRenderer> ().bounds;
		} else if (target.GetComponentInChildren<SkinnedMeshRenderer> () != null) {
			b = target.GetComponentInChildren<SkinnedMeshRenderer> ().bounds;
		}
			

		pts [0] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
		pts [1] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
		pts [2] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
		pts [3] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
		pts [4] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
		pts [5] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
		pts [6] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
		pts [7] = Camera.main.WorldToScreenPoint (new Vector3 (b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

		//Get them in GUI space
		for (int i = 0; i < pts.Length; i++)
			pts [i].y = Screen.height - pts [i].y;

		//Calculate the min and max positions
		Vector3 min = pts [0];
		Vector3 max = pts [0];
		for (int i = 1; i < pts.Length; i++) {
			min = Vector3.Min (min, pts [i]);
			max = Vector3.Max (max, pts [i]);
		}

		//Construct a rect of the min and max positions and apply some margin
		Rect r = Rect.MinMaxRect (min.x, min.y, max.x, max.y);
		r.xMin -= margin;
		r.xMax += margin;
		r.yMin -= margin;
		r.yMax += margin;

		//Render the box
		GUI.Box (r, "");
	}

}
