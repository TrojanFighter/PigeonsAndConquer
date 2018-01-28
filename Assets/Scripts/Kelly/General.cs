using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : DragableObjects {

	public override void Update() {
		// If we've selected to move the general and selected another point 
		if (Input.GetMouseButtonDown (0) && movementDecision) {
			moving = true;
			moveTo = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			moveTo.z = transform.position.z;
			movementDecision = false;
		}

		if (moving) {
			base.Move ();
		}
	}

}
