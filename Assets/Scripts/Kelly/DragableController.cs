using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{



	public class DragableController : MonoBehaviour
	{
		public Transform grabbedUnit;
		public bool mouseMode;
		public List<int> fingerIDs;
		public List<int> missingIDs;
		

		void Awake() {
			// CHANGE FOR PC/TOUCH
			mouseMode = true;
			fingerIDs = new List<int> ();
			missingIDs = new List<int> ();
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
						if (hits.Length > 0 && !fingerIDs.Contains (touch.fingerId)) {
							fingerIDs.Add (touch.fingerId);
							hits [0].transform.GetComponent<Unit> ().Grab (touch.fingerId);
						}
						missingIDs.Remove(touch.fingerId);
					}
				}
				foreach (int missingID in missingIDs) {
					fingerIDs.Remove (missingID);
				}
				foreach (int fID in fingerIDs) {
					missingIDs.Add (fID);
				}
			}
		}
	}
}
