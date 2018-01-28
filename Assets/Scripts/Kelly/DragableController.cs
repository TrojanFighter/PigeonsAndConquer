﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{



	public class DragableController : MonoBehaviour
	{
		public Transform grabbedUnit;
		public bool mouseMode;

		void Awake() {
			// CHANGE FOR PC/TOUCH
			mouseMode = true;
		}

		void Update()
		{
			if (mouseMode) {
				if (Input.GetMouseButtonDown (0)) {
					Vector2 inputPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					RaycastHit2D[] hits = Physics2D.RaycastAll (inputPos, inputPos);
					if (hits.Length > 0) {
						grabbedUnit = hits [0].transform;
						if (grabbedUnit.GetComponent<Unit>())
						{
							grabbedUnit.GetComponent<Unit>().Grab();
						}

					}
				}

				if (Input.GetMouseButtonUp (0) && grabbedUnit != null) {
					if (grabbedUnit.GetComponent<Unit>())
					{
						grabbedUnit.GetComponent<Unit>().MovementDecided();
						grabbedUnit = null;
					}

				}
			} else {
				if (Input.touches.Length > 0) {
					foreach (Touch touch in Input.touches) {
						Vector2 inputPos = Camera.main.ScreenToWorldPoint (touch.position);
						RaycastHit2D[] hits = Physics2D.RaycastAll (inputPos, inputPos);
						if (hits.Length > 0) {
							hits [0].transform.GetComponent<Unit> ().Grab (touch.fingerId);
						}
					}
				}
			}
		}
	}
}
