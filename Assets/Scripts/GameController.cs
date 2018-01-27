using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject generalPrefab;
	public GameObject knightPrefab;
	public GameObject arrowPrefab;


	// Game setup
	void Awake() {
		GameObject newGeneral = Instantiate (generalPrefab) as GameObject;
		GameObject newKnight = Instantiate (knightPrefab) as GameObject;
		newKnight.GetComponent<ArmyUnit> ().Init (newGeneral, arrowPrefab);
		newKnight = Instantiate (knightPrefab) as GameObject;
		Vector3 pos = newKnight.transform.position;
		pos.y += 2;
		newKnight.transform.position = pos;
		newKnight.GetComponent<ArmyUnit> ().Init (newGeneral, arrowPrefab);
	}


}
