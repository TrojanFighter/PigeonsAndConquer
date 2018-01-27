using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lords
{
	public abstract class Unit : MonoBehaviour
	{
		/*
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}*/
		public int UnitID;
		
		public GlobalDefine.UnitClass unitClass;
		public GlobalDefine.Fraction fraction,oppositeFraction;
		public GlobalDefine.UnitState unitState;
		public SoldierType soldierType;
		public Collider2D m_hitCollider;
		public AttackRangeCollider attackRangeCollider;
		public Transform m_transform;
		public Vector2 m_destination;
		public float UnitSpeed=1;
		public List<Unit> TargetUnitList;
		public int normalHP;
		protected MeshRenderer meshRenderer;
		protected Material originMaterial;



		public delegate void informUnitDieDelegate(Unit unit);

		public event informUnitDieDelegate informingUnitDieOrBetray;



		public virtual void OnEnable()
		{
			//Init ();
		}


		public virtual void Init()
		{
			m_transform = this.transform;
			if (attackRangeCollider == null)
			{
				attackRangeCollider = m_transform.Find("AttackRangeCollider").GetComponent<AttackRangeCollider>();
			}

			if (m_hitCollider == null)
			{
				m_hitCollider = GetComponent<Collider2D>();
			}


			soldierType = GameManager.SoldierTypes[(int) unitClass];
			attackRangeCollider.myunitclass = unitClass;
			SetAttackRange(soldierType.AttackRange);
			normalHP = soldierType.NormalHP;
			//Debug.Log (soldierType.AttackRange);

			unitState = GlobalDefine.UnitState.Advance;

			TargetUnitList = new List<Unit>();

			attackRangeCollider.AddingTargetUnitList += AddTargetUnitList;
			attackRangeCollider.RemovingTargetUnitList += RemoveTargetUnitList;

			meshRenderer = this.GetComponent<MeshRenderer>();
			originMaterial = meshRenderer.material;

		}



		public virtual void SetFraction(GlobalDefine.Fraction toFraction)
		{
			if (toFraction == GlobalDefine.Fraction.One)
			{
				fraction = GlobalDefine.Fraction.One;
				oppositeFraction = GlobalDefine.Fraction.Two;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction1Tag;
				attackRangeCollider.myfraction = GlobalDefine.Fraction.One;
			}
			else if (toFraction == GlobalDefine.Fraction.Two)
			{
				fraction = GlobalDefine.Fraction.Two;
				oppositeFraction = GlobalDefine.Fraction.One;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction2Tag;
				attackRangeCollider.myfraction = GlobalDefine.Fraction.Two;
			}
			else
			{
				fraction = GlobalDefine.Fraction.Netrual;
				oppositeFraction = GlobalDefine.Fraction.Netrual;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction0Tag;
				attackRangeCollider.myfraction = GlobalDefine.Fraction.Netrual;
			}
		}

		public virtual void SetAttackRange(float attackRange)
		{
			attackRangeCollider.transform.localPosition = new Vector3(0, 0, attackRange / 2);
			attackRangeCollider.transform.localScale = new Vector3(5, 1, attackRange);
		}

		public virtual void AddTargetUnitList(Unit other)
		{
			int prevUnitListCount = TargetUnitList.Count;
			TargetUnitList.Add(other);
			if (prevUnitListCount == 0 && TargetUnitList.Count == 1)
			{
				if (soldierType.AttackType == 3)
				{
					//信仰攻击
					unitState = GlobalDefine.UnitState.Attack;
					StartAttackTargetList();

				}

				if (soldierType.AttackType == 1 || soldierType.AttackType == 2)
				{
					//普通攻击
					unitState = GlobalDefine.UnitState.Attack;
					StartAttackClosestTarget();

				}
			}

		}

		public virtual void RemoveTargetUnitList(Unit other)
		{
			TargetUnitList.Remove(other);
			if (TargetUnitList.Count == 0)
			{
				try
				{
					StopAllCoroutines();
					unitState = GlobalDefine.UnitState.Advance;
				}
				catch
				{
					throw;
				}
			}
		}

		/*public void OnTriggerEnter(Collider other){
			
		}*/
		public virtual void StartAttackTargetList()
		{
			//持续攻击全体
			StartCoroutine(AttackTargetListOnce(soldierType.AttackTime));
		}

		IEnumerator AttackTargetListOnce(float attackTime)
		{
			//单次攻击全体
			while (true)
			{
				/*if (soldierType.AttackType == 1) {//近程攻击不打子弹
				foreach (Unit u in TargetUnitList) {
					u.TakeNormalAttack (soldierType.NormalAttackPower);
				}
			}
				if (soldierType.AttackType == 2) {//远程攻击要打子弹
					foreach (Unit u in TargetUnitList) {
						u.TakeNormalAttack (soldierType.NormalAttackPower);
					}
				}*/

				if (soldierType.AttackType == 3)
				{
					//信仰攻击
					/*if(TargetUnitList.Count>0){
						for (int i = 0; i < TargetUnitList.Count; i++) {
							TargetUnitList[i].TakeLoyaltyAttack (soldierType.LoyaltyAttackPower, this.fraction);
							InAudio.PostEvent(gameObject, GameManager.Instance.ShamanAttackEvent);
						}
	
					}*/
					/*foreach (Unit u in TargetUnitList) {
						u.TakeLoyaltyAttack (soldierType.LoyaltyAttackPower, this.fraction);
					}*/

				}

				yield return new WaitForSeconds(attackTime);
			}
		}

		public virtual void StartAttackClosestTarget()
		{
			StartCoroutine(AttackClosestTargetOnce(soldierType.AttackTime));

		}

		IEnumerator AttackClosestTargetOnce(float attackTime)
		{
			while (true)
			{
				float closestDistance = 100f;
				int closestTargetNum = 0;
				if (TargetUnitList.Count == 0)
					break;
				else
				{
					for (int i = 0; i < TargetUnitList.Count; i++)
					{
						float targetDistance = Vector3.Distance(this.transform.position, TargetUnitList[i].transform.position);
						if (closestDistance >= targetDistance)
						{
							closestDistance = targetDistance;
							closestTargetNum = i;
						}
					}

					if (soldierType.AttackType == 1)
					{
						//近程攻击不打子弹
						TargetUnitList[closestTargetNum].TakeNormalAttack(soldierType.NormalAttackPower);
						InAudio.PostEvent(gameObject, GameManager.Instance.KnightAttackEvent);
					}

					if (soldierType.AttackType == 2)
					{
						//远程攻击要打子弹
						TargetUnitList[closestTargetNum].TakeNormalAttack(soldierType.NormalAttackPower);
						InAudio.PostEvent(gameObject, GameManager.Instance.ArcherAttackEvent);
					}
				}

				//Debug.Log ("AttackOnce: " + soldierType.NormalAttackPower);
				yield return new WaitForSeconds(attackTime);
			}
		}



		public virtual void TakeNormalAttack(int hpAttack)
		{
			if (hpAttack > soldierType.ArmorAgainstNormalAttack)
			{
				normalHP -= hpAttack - soldierType.ArmorAgainstNormalAttack;
			}

			if (normalHP <= 0)
			{
				UnitDie();
			}
		}

		protected virtual void UnitDie()
		{
			if (informingUnitDieOrBetray != null)
			{
				informingUnitDieOrBetray(this);
			}

			Destroy(this.gameObject);
		}




		public virtual void UnitSabotaged()
		{
			GameObject ray;
			if (fraction == GlobalDefine.Fraction.One)
			{
				ray = GameManager.Instance.GodRayPurple;
				GameObject go = Instantiate(ray, this.transform.position, this.transform.rotation) as GameObject;
				Destroy(go, 1f);

			}
			else if (fraction == GlobalDefine.Fraction.Two)
			{
				ray = GameManager.Instance.GodRayGreen;
				GameObject go = Instantiate(ray, this.transform.position, this.transform.rotation) as GameObject;
				Destroy(go, 1f);
			}
			else
			{
				//ray
			}


			//UnitDie ();
			if (informingUnitDieOrBetray != null)
			{
				informingUnitDieOrBetray(this);
			}

			Destroy(this.gameObject);
		}

		public virtual void FixedUpdate()
		{

		}

		public void ReceiveCommand()
		{
		}

	}
}
