using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lords
{
	public abstract class Unit : MonoBehaviour
	{

		public int UnitID;
		
		public GlobalDefine.UnitClass unitClass;
		public GlobalDefine.Fraction fraction,oppositeFraction;
		public GlobalDefine.UnitState unitState;
		public SoldierType soldierType;
		public Collider2D m_hitCollider;
		public AttackRangeCollider attackRangeCollider;
		public Transform m_transform;
		public Vector3 m_destination;
		public float UnitSpeed=1;
		public List<Unit> TargetUnitList;
		public int normalHP;
		//protected MeshRenderer meshRenderer;
		//protected Material originMaterial;
		

		public delegate void informUnitDieDelegate(Unit unit);

		public event informUnitDieDelegate informingUnitDieOrBetray;

		public GameObject m_currentArrow;
		public int touchFingerId;
		public bool mouseMode,isBeingDragged;



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


			soldierType = DataManager.SoldierTypes[(int) unitClass];
			attackRangeCollider.myunitclass = unitClass;
			//SetAttackRange(soldierType.AttackRange);
			normalHP = soldierType.NormalHP;
			//Debug.Log (soldierType.AttackRange);

			unitState = GlobalDefine.UnitState.Advancing;

			TargetUnitList = new List<Unit>();

			attackRangeCollider.AddingTargetUnitList += AddTargetUnitList;
			attackRangeCollider.RemovingTargetUnitList += RemoveTargetUnitList;

			//meshRenderer = this.GetComponent<MeshRenderer>();
			//originMaterial = meshRenderer.material;

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
			return;
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
					//unitState = GlobalDefine.UnitState.Attack;
					StartAttackTargetList();

				}

				if (soldierType.AttackType == 1 || soldierType.AttackType == 2)
				{
					//普通攻击
					//unitState = GlobalDefine.UnitState.Attack;
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
					unitState = GlobalDefine.UnitState.Advancing;
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
						InAudio.PostEvent(gameObject, SceneManager.Instance.KnightAttackEvent);
					}

					if (soldierType.AttackType == 2)
					{
						//远程攻击要打子弹
						TargetUnitList[closestTargetNum].TakeNormalAttack(soldierType.NormalAttackPower);
						InAudio.PostEvent(gameObject, SceneManager.Instance.ArcherAttackEvent);
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
			/*GameObject ray;
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
			}*/


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

		void MouseLogic()
		{
			// Mouse logic
			if (mouseMode) {
				if (Input.GetMouseButtonDown (0)) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hit;
					if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
						isBeingDragged = true;
						m_currentArrow = Instantiate (SceneManager.Instance.arrowPrefab) as GameObject;
						GetComponent<LineRenderer> ().enabled = true;

					}
				}

				if (isBeingDragged) {
					Vector3 arrowPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					arrowPos.z = -1f;
					m_currentArrow.transform.position = arrowPos;
					GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, arrowPos });
					if (Input.GetMouseButtonUp (0)) {
						isBeingDragged = false;
						if(m_currentArrow!=null)
						Destroy (m_currentArrow);
						GetComponent<LineRenderer> ().enabled = false;
					}
				}
			} else {
				// Touch logic
				if (isBeingDragged) {
					if (Input.touches.Length == 0) {
						// Dragging has ended
						MovementDecided ();
					}
					for(int i=0; i < Input.touches.Length; i++) {
						if (Input.touches [i].fingerId == touchFingerId) {
							TouchDrag (Input.touches [i]);
							break;
						} else if (i == Input.touches.Length - 1) {
							// Dragging has ended
							MovementDecided();
						}
					}
				} else {
					// Check to see if we're touching this object
					foreach (Touch touch in Input.touches) {
						Ray ray = Camera.main.ScreenPointToRay (touch.position);
						RaycastHit hit;
						if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
							isBeingDragged = true;
							touchFingerId = touch.fingerId;
							m_currentArrow = Instantiate (SceneManager.Instance.arrowPrefab) as GameObject;
							GetComponent<LineRenderer> ().enabled = true;
						}
					}
				}
			}
		}
		
		public void MovementDecided() {
			isBeingDragged = false;
			if(m_currentArrow!=null)
				Destroy (m_currentArrow);
			GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, transform.position }); // Resets line to 0
			GetComponent<LineRenderer> ().enabled = false;
			// At this point we need to generate a messenger from the general
		}

		public void TouchDrag(Touch currentTouch) {
			Vector3 arrowPos = Camera.main.ScreenToWorldPoint (currentTouch.position);
			arrowPos.z = -1f;
			m_destination = arrowPos;
			m_currentArrow.transform.position = arrowPos;
			GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, arrowPos });
		}
		

		public void Move() {
			float step = UnitSpeed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, m_destination, step);
			if (transform.position == m_destination) {
				//moving = false;
				unitState = GlobalDefine.UnitState.Standing;
			}
		}


		//Happens when a messenger Reaches the unit. Only the non-direct-controlled unit with correct id will accept it.
		public bool ReceiveCommand(Command receivedCommand)
		{
			if (soldierType.CommandType == (int) GlobalDefine.CommandType.Unable)
			{
				return false;
			}

			if (receivedCommand.m_TargetUnitID != UnitID)
			{
				return false;
			}

			m_destination = receivedCommand.m_commandTargetPostion;
			if (unitState == GlobalDefine.UnitState.Standing)
			{
				unitState = GlobalDefine.UnitState.Advancing;
			}

			return true;
		}

		public void SelfDestroy()
		{
			Destroy(this.gameObject);
		}

		public void StartPursuingTarget(int targetID)
		{
			
		}

	}
}
