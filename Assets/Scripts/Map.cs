using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates the map
public class Map : MonoBehaviour {
	public GameObject groundSpritePrefab;
	private int mapDimension = 18;

	void Awake() {
		// Generates a square map of size mapDimension
		for (int i = 0; i < mapDimension; i++) {
			for (int j = 0; j < mapDimension; j++) {
				GameObject newTile = Instantiate (groundSpritePrefab) as GameObject;
				Vector3 pos = newTile.transform.position;
				pos.x += i;
				pos.y += j;
				newTile.transform.position = pos;
			}
		}
	}
}
