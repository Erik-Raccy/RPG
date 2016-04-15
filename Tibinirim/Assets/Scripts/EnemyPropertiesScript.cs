using UnityEngine;
using System.Collections;
using System.Linq;
public class EnemyPropertiesScript : MonoBehaviour {
    public Utility Utility;
    private int Health { get; set; }
    private int MaxHealth { get; set; }
    private int Speed { get; set; }
    private int TargetLevel { get; set; }
    private int Damage { get; set; }
    Transform closeEnemy;
    private bool Target; 
    // Use this for initialization
    void Start () {
        MaxHealth = 1000;
        Speed = 2;
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

    
}
