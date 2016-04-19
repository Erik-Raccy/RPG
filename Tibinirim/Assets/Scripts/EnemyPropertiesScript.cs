using UnityEngine;
using System.Collections;
using System.Linq;
public class EnemyPropertiesScript : MonoBehaviour {
    public Utility Utility;

	public string Name;
    private int health { get; set; }
    private int MaxHealth { get; set; }
    private int Speed { get; set; }
    private int TargetLevel { get; set; }
    private int Damage { get; set; }

    Transform closeEnemy;
    private bool Target; 
    // Use this for initialization
    void Start () {
        MaxHealth = 1000;
        Speed = 1;
        TargetLevel = 10;
        Damage = 100;
        Utility = GetComponent<Utility>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindGameObjectsWithTag("Player").Any() && !Target){
            closeEnemy = Utility.GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player").Select(x => x.transform).ToArray());
            Debug.Log(closeEnemy.gameObject);
            Target = true;
        }
        
        
    }

	public void hit(int damage = 1) {
		health = (health - damage > 0) ? (health - damage) : 0;
		//UI.setHealthSliderValue (health);
	}
    
}
