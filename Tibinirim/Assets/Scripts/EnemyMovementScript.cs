using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyMovementScript : MonoBehaviour {

    public Utility Utility;
    EnemyPropertiesScript Properties;

    Transform closeEnemy;
    private bool Target;
    private Vector3 targetVector;

    private float increment;
    private bool isMoving;
    private float speedModifier;

    private Vector3 startPoint;
    private Vector3 endPoint;


    // Use this for initialization
    void Start () {
        startPoint = transform.position;
        endPoint = transform.position;

        speedModifier = 1;
        Utility = GetComponent<Utility>();
        Properties = GetComponent<EnemyPropertiesScript>();
    }
	
	// Update is called once per frame
	void Update () {


        if (GameObject.FindGameObjectsWithTag("Player").Any() && !Target)
        {
            closeEnemy = Utility.GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player").Select(x => x.transform).ToArray());
            Target = true;
        }


        //Movement Code Block
        if (increment <= 1 && isMoving)
        {
            increment += Properties.GetSpeed() / (100 * speedModifier);
        }
        else {
            isMoving = false;
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, increment);
        }
        
        
        if (Target && closeEnemy.position.y == transform.position.y && !isMoving)
        {
            if (Vector3.Distance(closeEnemy.position, transform.position) > 1.6 && Vector3.Distance(closeEnemy.position, transform.position) < 10.6) //player is in sight, chase it
            {
                targetVector = closeEnemy.position - transform.position;
                Vector3.Normalize(targetVector);

                //ISSUE: problems pathing downwards
                if (Mathf.Abs(targetVector.x) >= targetVector.z && targetVector.x > 0)
                {
                    //move right
                    transform.GetChild(0).forward = Vector3.right;
                    endPoint = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    increment = 0;
                    isMoving = true;
                    speedModifier = 1;
                    startPoint = transform.position;
                }
                else if (Mathf.Abs(targetVector.x) >= targetVector.z && targetVector.x < 0)
                {
                    //move left
                    transform.GetChild(0).forward = Vector3.left;
                    endPoint = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    increment = 0;
                    isMoving = true;
                    speedModifier = 1;
                    startPoint = transform.position;
                }
                else if (Mathf.Abs(targetVector.z) > targetVector.x && targetVector.z > 0)
                {
                    //move up
                    transform.GetChild(0).forward = Vector3.forward;
                    endPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    increment = 0;
                    isMoving = true;
                    speedModifier = 1;
                    startPoint = transform.position;
                }
                else if (Mathf.Abs(targetVector.z) > targetVector.x && targetVector.z < 0)
                {
                    //move down
                    transform.GetChild(0).forward = Vector3.back;
                    endPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    increment = 0;
                    isMoving = true;
                    speedModifier = 1;
                    startPoint = transform.position;
                }
                else
                {
                    //wtf
                }
            }
            else if (Vector3.Distance(closeEnemy.position, transform.position) < 1.6) // player is in range, hit it
            {
                Properties.Attack(closeEnemy.GetComponent<PlayerStatsScript>());
            }
            else if (Vector3.Distance(closeEnemy.position, transform.position) > 10.6) // player is way out of range and escaped, move back to spawn point
            {
                closeEnemy = null;
            }
        }
        else
        {
            Target = false;
            closeEnemy = null;
        }

    }
}
