using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
public class EnemyPropertiesScript : MonoBehaviour {
    
	public Utility Utility;

    //Camera camera;
    //Vector3 screenPosition;

    public string name;

    private int health { get; set; }
    private int maxHealth { get; set; }
    private int Speed { get; set; }
    private int targetLevel { get; set; }
    private int Damage { get; set; }

    Transform closeEnemy;
    private bool Target;

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
        Utility = GetComponent<Utility>();

        setHealthSliderValue(health);
        setHealthSliderMaxValue(maxHealth);


    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
            OnDeath();


        if(GameObject.FindGameObjectsWithTag("Player").Any() && !Target){
            closeEnemy = Utility.GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player").Select(x => x.transform).ToArray());
            Debug.Log(closeEnemy.gameObject);
            Target = true;
        }
        
        
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

    void OnDeath()
    {
        Destroy(gameObject);
    }

}
