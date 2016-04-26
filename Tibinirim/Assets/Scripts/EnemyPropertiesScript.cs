using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyPropertiesScript : MonoBehaviour {

    //Camera camera;
    //Vector3 screenPosition;

    public new string name;

    private int health { get; set; }
    private int maxHealth { get; set; }
    private int Speed { get; set; }
    private int targetLevel { get; set; }
    private int Damage { get; set; }
    private float attackRate = 3;

    private float attackRateTimer = 0;

    public Slider healthSlider;
    public Text healthText;
    public Text namePlate;


    // Use this for initialization
    void Start () {
        namePlate.text = name;

        health = 100;
        maxHealth = 100;
        Speed = 1;
        targetLevel = 10;
        Damage = 100;

        setHealthSliderValue(health);
        setHealthSliderMaxValue(maxHealth);

    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
            OnDeath();

        if (attackRateTimer <= attackRate)
            attackRateTimer = attackRateTimer + Time.deltaTime;

    }

	void OnGUI () {
        /*
        if (Camera.main != null) {
			screenPosition = Camera.main.WorldToScreenPoint (transform.position);// gets screen position.
			screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y
			Rect rect = new Rect (screenPosition.x - 50, screenPosition.y - 100, 100, 24);// makes a rect centered at the player ( 100x24 )
			GUI.Box (rect, Name);
			GUI.Slider(rect, health/MaxHealth, 1, 0, 100, slider, 

		}
        */
	}

	public void hit(int damage = 1) {
		health = (health - damage > 0) ? (health - damage) : 0;
        setHealthSliderValue(health);
    }

    void setHealthSliderValue(int inHealth)
    {
        healthSlider.value = inHealth;
        healthText.text = inHealth + " / " + healthSlider.maxValue;
    }

    void setHealthSliderMaxValue(int inMaxHealth)
    {
        healthSlider.maxValue = inMaxHealth;
        healthText.text = healthSlider.value + " / " + inMaxHealth;
    }

    public int GetSpeed()
    {
        return Speed;
    }

    public void Attack(PlayerStatsScript player)
    {
        if (attackRateTimer > attackRate)
        {
            player.hit(Damage);
            attackRateTimer = 0;
        }
          
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }

}
