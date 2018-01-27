using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject generalPrefab;
	public GameObject knightPrefab;
	public GameObject arrowPrefab;

	Vector3 touchPos;

	//Change me to change the touch phase used.
	TouchPhase touchPhase = TouchPhase.Ended;

	// Game setup
	void Awake() {
		GameObject newGeneral = Instantiate (generalPrefab) as GameObject;
		GameObject newKnight = Instantiate (knightPrefab) as GameObject;
		newKnight.GetComponent<ArmyUnit> ().Init (newGeneral, arrowPrefab);
	}

	// Get the raycast hit of units
	void Update() {
//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
//			//We transform the touch position into word space from screen space and store it.
//			touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//
//			Vector3 touchPosWorld = new Vector3(touchPosWorld.x, touchPosWorld.y, 0);
//
			//We now raycast with this information. If we have hit something we can process it.
//			RaycastHit hitInformation = Physics.Raycast(touchPosWorld, Camera.main.transform.forward);
//
//			if (hitInformation.collider != null) {
//				//We should have hit something with a 2D Physics collider!
//				GameObject touchedObject = hitInformation.transform.gameObject;
//				//touchedObject should be the object someone touched.
//				print("Touched " + touchedObject.transform.name);
//			}
//		}
			

	}
}
