using UnityEngine;
using System.Collections;

namespace Lords
{
	public class AttackRangeCollider : MonoBehaviour
	{
		/*
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}*/
		public GlobalDefine.Fraction myfraction;
		public GlobalDefine.UnitClass myunitclass;
		public Unit myunit;

		public delegate void AddTargetUnitListDelegate(Unit unit);

		public delegate void RemoveTargetUnitListDelegate(Unit unit);

		public event AddTargetUnitListDelegate AddingTargetUnitList;
		public event RemoveTargetUnitListDelegate RemovingTargetUnitList;

		/*public delegate void AddHealingLoyaltyUnitListDelegate(Unit unit);
		public delegate void RemoveHealingLoyaltyUnitListDelegate(Unit unit);
		public event AddHealingLoyaltyUnitListDelegate AddingHealingUnitList;
		public event RemoveHealingLoyaltyUnitListDelegate RemovingHealingUnitList;*/

		void Awake()
		{
			myunit = this.transform.GetComponentInParent<Unit>();
		}

		void Start()
		{

		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<Unit>() && other.GetComponent<Unit>() != myunit)
			{

				if (myfraction == GlobalDefine.Fraction.One && other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.Two)
				{
					//myunit.SendMessage ("AddTargetUnitList",other.GetComponent<Unit>());
					other.GetComponent<Unit>().informingUnitDieOrBetray += this.BeingInformedUnitDie;
					if (AddingTargetUnitList != null)
					{
						AddingTargetUnitList(other.GetComponent<Unit>());
					}
				}
				else if (myfraction == GlobalDefine.Fraction.Two &&
				         other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.One)
				{
					//myunit.SendMessage ("AddTargetUnitList",other.GetComponent<Unit>());
					other.GetComponent<Unit>().informingUnitDieOrBetray += this.BeingInformedUnitDie;
					if (AddingTargetUnitList != null)
					{
						AddingTargetUnitList(other.GetComponent<Unit>());
					}
				}
			}


		}

		void OnTriggerStay2D(Collider2D other)
		{

		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.GetComponent<Unit>() && other.GetComponent<Unit>() != myunit)
			{
				if (myfraction == GlobalDefine.Fraction.One && other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.Two)
				{
					//myunit.SendMessage ("RemoveTargetUnitList",other.GetComponent<Unit>());
					other.GetComponent<Unit>().informingUnitDieOrBetray -= this.BeingInformedUnitDie;
					if (RemovingTargetUnitList != null)
					{
						RemovingTargetUnitList(other.GetComponent<Unit>());
					}

				}
				else if (myfraction == GlobalDefine.Fraction.Two &&
				         other.GetComponent<Unit>().fraction == GlobalDefine.Fraction.One)
				{
					//myunit.SendMessage ("RemoveTargetUnitList",other.GetComponent<Unit>());
					other.GetComponent<Unit>().informingUnitDieOrBetray -= this.BeingInformedUnitDie;
					if (RemovingTargetUnitList != null)
					{
						RemovingTargetUnitList(other.GetComponent<Unit>());
					}

				}

			}
		}

		public void BeingInformedUnitDie(Unit other)
		{
			if (other.GetComponent<Unit>() && other.GetComponent<Unit>() != myunit)
			{
				other.GetComponent<Unit>().informingUnitDieOrBetray -= this.BeingInformedUnitDie;
				/*if (myfraction == GlobalDefine.Fraction.One && other.gameObject.GetComponent<Unit>().fraction==GlobalDefine.Fraction.Two) {
					//myunit.SendMessage ("RemoveTargetUnitList",other.GetComponent<Unit>());
					if (RemovingTargetUnitList != null) {
						RemovingTargetUnitList (other.GetComponent<Unit> ());
					}
				}
				if (myfraction == GlobalDefine.Fraction.Two && other.gameObject.GetComponent<Unit>().fraction==GlobalDefine.Fraction.One) {
					//myunit.SendMessage ("RemoveTargetUnitList",other.GetComponent<Unit>());
					if (RemovingTargetUnitList != null) {
						RemovingTargetUnitList (other.GetComponent<Unit> ());
					}
				}*/
				if (RemovingTargetUnitList != null)
				{
					RemovingTargetUnitList(other.GetComponent<Unit>());
				}
			}
		}
	}
}