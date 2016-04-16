using UnityEngine;
using System.Collections;

public class PlayerStatsScript : MonoBehaviour {

	private int level;
	private string vocation;

	private int maxHealth = 1000;
	private int health = 1000;

	private int maxMana = 1000;
	private int mana = 1000;

	private int maxStamana = 1000;
	private int stamana = 1000;

	private OverHeadUIScript UI;

	// Use this for initialization
	void Start () {

		UI = GetComponent<OverHeadUIScript> ();

		//initialize UI values
		UI.setHealthSliderMaxValue (maxHealth);
		UI.setHealthSliderValue (health);

		UI.setManaSliderMaxValue (maxMana);
		UI.setManaSliderValue (mana);

		UI.setStamanaSliderMaxValue (maxStamana);
		UI.setStamanaSliderValue (stamana);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void hit(int damage = 1) {
		health = (health - damage > 0) ? (health - damage) : 0;
		UI.setHealthSliderValue (health);
	}
	public void heal(int healing = 100)
	{
		health = (health + healing <= maxHealth) ? health + healing : maxHealth;
		UI.setHealthSliderValue (health);
	}
	public void fullHeal()
	{
		health = maxHealth;
		UI.setHealthSliderValue (health);
	}
}
