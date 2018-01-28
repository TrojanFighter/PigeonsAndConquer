﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{



	public class DragableController : MonoBehaviour
	{
		public Transform grabbedUnit;

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D[] hits = Physics2D.RaycastAll(inputPos, inputPos);
				if (hits.Length > 0)
				{
					grabbedUnit = hits[0].transform;
					grabbedUnit.GetComponent<Unit>().Grab();
				}
			}

			if (Input.GetMouseButtonUp(0) && grabbedUnit != null)
			{
				grabbedUnit.GetComponent<Unit>().MovementDecided();
				grabbedUnit = null;
			}

			if (Input.touches.Length > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					Vector2 inputPos = Camera.main.ScreenToWorldPoint(touch.position);
					RaycastHit2D[] hits = Physics2D.RaycastAll(inputPos, inputPos);
					if (hits.Length > 0)
					{
						hits[0].transform.GetComponent<Unit>().Grab(touch.fingerId);
					}
				}
			}
		}
	}
}
