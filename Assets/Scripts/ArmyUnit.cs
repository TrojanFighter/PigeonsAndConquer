using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyUnit : MonoBehaviour {
	public bool movementDecision = false;
	public bool moving = false;
	public float speed = 2f;
	public Vector3 moveTo;

	public GameObject myGeneral;

	public void Init(GameObject general) {
		myGeneral = general;
	}

	// 
	void OnMouseUp() {
		if (movementDecision == false) {
			movementDecision = true;
			if (myGeneral != null) {
				print ("Gen position: " + myGeneral.transform.position + " my position: " + transform.position);
			}
		} else {
			// TODO: Add cancel icon instead of assuming second left click cancels decision
			movementDecision = false;
		}
	}

	public virtual void Update() {
		if (Input.GetMouseButtonDown (0) && movementDecision) {
			
		}
		if (moving) {
			Move ();
		}

	}

	public void Move() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, moveTo, step);
		if (transform.position == moveTo) {
			moving = false;
		}
	}
}
