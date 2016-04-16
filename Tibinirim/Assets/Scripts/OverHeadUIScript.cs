using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverHeadUIScript : MonoBehaviour {
	private Vector3 screenPosition;
	Camera camera;

	//Sliders and text for UI imported via editor
	public Slider healthSlider;
	public Text healthText;

	public Slider manaSlider;
	public Text manaText;

	public Slider stamanaSlider;
	public Text stamanaText;

	// Use this for initialization
	void Start () {
		camera = GetComponentInChildren<Camera> ();
	}
	
	// find player location on screen from camera and draw their name tag
	void OnGUI () {
		screenPosition = camera.WorldToScreenPoint(transform.position);// gets screen position.
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

}
