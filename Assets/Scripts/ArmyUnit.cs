﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmyUnit : MonoBehaviour {
	public bool movementDecision = false;
	public bool moving = false;
	public bool dragging = false;
	public float speed = 2f;
	public Vector3 moveTo;

	public GameObject myGeneral;
	public GameObject arrowPrefab;
	public GameObject currentArrow;

	public bool mouseMode;

	public int touchFingerId;

	void Awake() {
		mouseMode = (Application.platform == RuntimePlatform.OSXEditor);
	}

	public void Init(GameObject general, GameObject arPrefab) {
		myGeneral = general;
		arrowPrefab = arPrefab;
	}

	// 
	void OnMouseUp() {
//		if (movementDecision == false) {
//			movementDecision = true;
//			if (myGeneral != null) {
//				print ("Gen position: " + myGeneral.transform.position + " my position: " + transform.position);
//			}
//		} else {
//			// TODO: Add cancel icon instead of assuming second left click cancels decision
//			movementDecision = false;
//		}
	}

	public virtual void Update() {

		// Mouse logic
		if (mouseMode) {
//			if (Input.GetMouseButtonDown (0)) {
//				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//				RaycastHit hit;
//				if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
//					dragging = true;
//					currentArrow = Instantiate (arrowPrefab) as GameObject;
//					GetComponent<LineRenderer> ().enabled = true;
//
//				}
//			}
//
//			if (dragging) {
//				Vector3 arrowPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//				arrowPos.z = 5f;
//				currentArrow.transform.position = arrowPos;
//				GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, arrowPos });
//				if (Input.GetMouseButtonUp (0)) {
//					dragging = false;
//					Destroy (currentArrow);
//					GetComponent<LineRenderer> ().enabled = false;
//				}
//			}
		} else {
			// Touch logic
			if (dragging) {
				if (Input.touches.Length == 0) {
					// Dragging has ended
					MovementDecided ();
				}
				for(int i=0; i < Input.touches.Length; i++) {
					if (Input.touches [i].fingerId == touchFingerId) {
						TouchDrag (Input.touches [i]);
						break;
					} else if (i == Input.touches.Length - 1) {
						// Dragging has ended
						MovementDecided();
					}
				}
			} else {
				// Check to see if we're touching this object
				foreach (Touch touch in Input.touches) {
					Ray ray = Camera.main.ScreenPointToRay (touch.position);
					RaycastHit hit;
					if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
						dragging = true;
						touchFingerId = touch.fingerId;
						currentArrow = Instantiate (arrowPrefab) as GameObject;
						GetComponent<LineRenderer> ().enabled = true;
					}
				}
			}
		}
	}

	public void MovementDecided() {
		dragging = false;
		Destroy (currentArrow);
		GetComponent<LineRenderer> ().enabled = false;
		// At this point we need to generate a messenger from the general
	}

	public void TouchDrag(Touch currentTouch) {
		Vector3 arrowPos = Camera.main.ScreenToWorldPoint (currentTouch.position);
		arrowPos.z = 5f;
		moveTo = arrowPos;
		currentArrow.transform.position = arrowPos;
		GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, arrowPos });
	}
		

	public void Move() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, moveTo, step);
		if (transform.position == moveTo) {
			moving = false;
		}
	}
}
