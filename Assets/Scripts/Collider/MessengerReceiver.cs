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
		public bool inited = false;
		
		void Init()
		{
			if (inited) return;
			if(myunit==null)
				myunit = this.transform.GetComponentInParent<Unit>();
			myfraction = myunit.fraction;
			myunitclass = myunit.unitClass;
			inited = true;
		}

		void Awake()
		{
			Init();
		}


		void OnTriggerEnter2D(Collider2D other)
		{
			Init();
			if (myunitclass==GlobalDefine.UnitClass.Messenger)
			{
				enabled = false;
				return;
			}

			if (other.GetComponent<MessengerUnit>() && other.GetComponent<MessengerUnit>() != myunit)
			{

				//Kill the opposite fraction Messenger
				if (myfraction!=other.GetComponent<MessengerUnit>().fraction )
				{
					other.GetComponent<MessengerUnit>().SelfDestroy();
					//myunit.SendMessage ("AddTargetUnitList",other.GetComponent<Unit>());

				}
				else if (other.GetComponent<MessengerUnit>()&&other.GetComponent<MessengerUnit>().m_targetUnitID==myunit.UnitID)
				{
					myunit.ReceiveCommand(other.GetComponent<MessengerUnit>().m_commandID);
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
