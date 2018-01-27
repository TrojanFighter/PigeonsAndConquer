using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject generalPrefab;
	public GameObject knightPrefab;

	// Game setup
	void Awake() {
		GameObject newGeneral = Instantiate (generalPrefab) as GameObject;
		GameObject newKnight = Instantiate (knightPrefab) as GameObject;
		newKnight.GetComponent<ArmyUnit> ().Init (newGeneral);
	}
}
