using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{
	public class MessengerReceiver : MonoBehaviour
	{
		public GlobalDefine.Fraction myfraction;
		public GlobalDefine.UnitClass myunitclass;
		public Unit myunit;
		
		void Awake()
		{
			if(myunit==null)
				myunit = this.transform.GetComponentInParent<Unit>();
		}

		
		void OnTriggerEnter2D(Collider2D other)
		{
			if (GetComponent<MessengerUnit>())
			{
				return;
			}

			if (other.GetComponent<MessengerUnit>() && other.GetComponent<MessengerUnit>() != myunit)
			{

				if (myfraction == GlobalDefine.Fraction.One && other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.Two)
				{
					//myunit.SendMessage ("AddTargetUnitList",other.GetComponent<Unit>());

				}
				else if (myfraction == GlobalDefine.Fraction.Two &&
				         other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.One)
				{
					//myunit.SendMessage ("AddTargetUnitList",other.GetComponent<Unit>());
				}
			}


		}

		void OnTriggerStay2D(Collider2D other)
		{

		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (GetComponent<MessengerUnit>())
			{
				return;
			}
			if (other.GetComponent<Unit>() && other.GetComponent<Unit>() != myunit)
			{
				if (myfraction == GlobalDefine.Fraction.One && other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.Two)
				{


				}
				else if (myfraction == GlobalDefine.Fraction.Two &&
				         other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.One)
				{


				}

			}
		}

	}
}
