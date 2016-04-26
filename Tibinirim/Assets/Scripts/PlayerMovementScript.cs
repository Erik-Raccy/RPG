using UnityEngine;
using System.Collections;



public class PlayerMovementScript : MonoBehaviour {

	private Vector3 startPoint;
	private Vector3 endPoint;

    private Vector3 rayCastPoint;

	private float speed;
	private float speedModifier;
	private float increment;
	private bool isMoving;

	private Animator animator;

	private GameObject model;

	private PlayerStatsScript stats;

	// Use this for initialization
	void Start () {
        speed = 3;
		speedModifier = 1;
		increment = 0;
		isMoving = false;
		startPoint = transform.position;
		endPoint = transform.position;

		stats = GetComponent<PlayerStatsScript> ();
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<NetworkView>().isMine)
        {
            
            //Movement Code Block
            if (increment <= 1 && isMoving)
            {
                increment += speed / (100 * speedModifier);
            }
            else {
                isMoving = false;
            }

            if (isMoving)
            {
                transform.position = Vector3.Lerp(startPoint, endPoint, increment);

            }

			if (Input.GetKey ("w") && isMoving == false) {
				animator.SetFloat ("Walk", 1);

				transform.GetChild (0).forward = Vector3.forward;
				endPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
				CheckNextTile (Vector3.forward);

			} else if (Input.GetKey ("s") && isMoving == false) {
				animator.SetFloat ("Walk", 1);

				transform.GetChild (0).forward = Vector3.back;
				endPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1);
				CheckNextTile (Vector3.back);

			} else if (Input.GetKey ("a") && isMoving == false) {
				animator.SetFloat ("Walk", 1);

				transform.GetChild (0).forward = Vector3.left;
				endPoint = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
				CheckNextTile (Vector3.left);

			} else if (Input.GetKey ("d") && isMoving == false) {
				animator.SetFloat ("Walk", 1);

				transform.GetChild (0).forward = Vector3.right;
				endPoint = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
				CheckNextTile (Vector3.right);
			
			}

			//if no movement keys are held down, and you have reached the next tile, cancel the walk animation
			if (!Input.GetKey ("w") && !Input.GetKey ("s") && !Input.GetKey ("a") && !Input.GetKey ("d") && increment >= 1) {
				animator.SetFloat ("Walk", 0);
			}
				

            //end Movement Block
        }
	}

	void CheckNextTile(Vector3 direction) {
		RaycastHit hit;
        rayCastPoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Debug.DrawRay (rayCastPoint, direction);
		if (Physics.Raycast (rayCastPoint, direction, out hit, 1))
        {
            //do nothing if terrain is inaccessable or an enemy
            if (hit.collider.gameObject.tag == "Enemy")
            {
            }
            if (hit.collider.gameObject.tag == "Blocked") {
			}
			//if you hit a ramp, adjust trajectory up or down
			else if (hit.collider.gameObject.tag == "Ramp Up")
            {
				increment = 0;
				isMoving = true;
				speedModifier = 3;
				startPoint = transform.position;
				endPoint = new Vector3 (endPoint.x + (endPoint.x - startPoint.x), endPoint.y + 1, endPoint.z + (endPoint.z - startPoint.z));
			}
            else if (hit.collider.gameObject.tag == "Ramp Down")
            {
				increment = 0;
				isMoving = true;
				speedModifier = 3;
				startPoint = transform.position;
				endPoint = new Vector3 (endPoint.x + (endPoint.x - startPoint.x), endPoint.y - 1, endPoint.z + (endPoint.z - startPoint.z));
			}
		}
        //otherwise, move normally
        else {
			increment = 0;
			isMoving = true;
			speedModifier = 1;
			startPoint = transform.position;
		}
	}

}



