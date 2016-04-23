using UnityEngine;
using System.Collections;

public class PlayerStatsScript : MonoBehaviour {

	private int level;
	private string vocation;

    private float attackRate = 3;
    private float attackRateTimer = 0;

	private int maxHealth = 1000;
	private int health = 1000;

	private int maxMana = 1000;
	private int mana = 1000;

	private int maxStamana = 1000;
	private int stamana = 1000;

	private OverHeadUIScript UI;

	private GameObject targetEnemy;
	Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponentInChildren<Camera> ();

		UI = GetComponent<OverHeadUIScript> ();

		//initialize UI values
		UI.setHealthSliderMaxValue (maxHealth);
		UI.setHealthSliderValue (health);

		UI.setManaSliderMaxValue (maxMana);
		UI.setManaSliderValue (mana);

		UI.setStamanaSliderMaxValue (maxStamana);
		UI.setStamanaSliderValue (stamana);

		//targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
		targetEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (attackRateTimer <= attackRate)
            attackRateTimer = attackRateTimer + Time.deltaTime;

		// if the player clicks on an enemy, select it as targeted. Only 1 enemy may be targeted at a time.
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction*10);
			if (Physics.Raycast (ray, out hit, 100)) {
				//clicking on an untarget enemy targets them
				if (hit.collider.transform.tag == "Enemy" && hit.transform.gameObject != targetEnemy) {
					targetEnemy = hit.transform.gameObject;
				}
				//clicking on an already targeted enemy untargets them
				else if (hit.collider.transform.tag == "Enemy" && hit.transform.gameObject == targetEnemy) {
					targetEnemy = null;
				}
			}
		}

		//if the targeted enemy is in range, hit it
		if (targetEnemy != null) {
			if (Vector3.Distance (targetEnemy.transform.position, transform.position) < 1.6 && attackRateTimer >= attackRate) {
				EnemyPropertiesScript enemyScript = targetEnemy.GetComponent<EnemyPropertiesScript> ();
				enemyScript.hit(25);
                attackRateTimer = 0;
			}
				
		}
			
				
	}

	void OnGUI () {
		if (targetEnemy != null) {
			UI.drawTargetBox (targetEnemy);
		}
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
