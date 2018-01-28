using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using UnityEditor;

namespace Lords
{
	public abstract class Unit : MonoBehaviour
	{

		public bool inited = false;	
		public int UnitID;
		
		public GlobalDefine.UnitClass unitClass;
		public GlobalDefine.Fraction fraction,oppositeFraction;
		public GlobalDefine.UnitState unitState;
		public SoldierType soldierType;
		public Collider2D m_hitCollider;
		public AttackRangeCollider attackRangeCollider;
		public Rigidbody2D m_rigidbody2D;
		public Transform m_transform, m_turnableRoot;
		public Vector3 m_commanddestination,m_patrollingdestination;
		public int m_pursueTargetID = -1;
		public float UnitSpeed=2,RotationSpeed = 5;
		public List<Unit> TargetUnitList;
		public int normalHP;
		public bool isDraggable = true;
		public bool bUnderPikeEffect = false;

		public float lastPikedTime = 0f, PikeRecoverTime = 0.5f,pikedSpeedModifier=0.3f;

		public MessengerReceiver m_MessengerReceiver;
		//protected MeshRenderer meshRenderer;
		//protected Material originMaterial;
		

		public delegate void informUnitDieDelegate(Unit unit);

		public event informUnitDieDelegate informingUnitDieOrBetray;

		public GameObject m_currentArrow;
		public int touchFingerId;
		public bool mouseMode,isBeingDragged;

		public int attackFrames;

		protected virtual void Awake(){
			//Init();
			mouseMode = false;
			attackFrames = -1;
		}

		protected virtual void OnEnable()
		{
			Init ();
		}


		public virtual void Init()
		{
			if (inited) return;
			
			m_transform = this.transform;
			if (attackRangeCollider == null)
			{
				attackRangeCollider = m_transform.Find("AttackRangeCollider").GetComponent<AttackRangeCollider>();
			}

			if (m_hitCollider == null)
			{
				m_hitCollider = GetComponent<Collider2D>();
			}

			m_rigidbody2D = GetComponent<Rigidbody2D>();
			soldierType = DataManager.SoldierTypes[(int) unitClass];
			//attackRangeCollider.myunitclass = unitClass;
			//SetAttackRange(soldierType.AttackRange);
			normalHP = soldierType.NormalHP;
			UnitSpeed = soldierType.NormalMoveSpeed;
			
			//Debug.Log (soldierType.AttackRange);

			unitState = GlobalDefine.UnitState.Standing;

			TargetUnitList = new List<Unit>();

			attackRangeCollider.AddingTargetUnitList += AddTargetUnitList;
			attackRangeCollider.RemovingTargetUnitList += RemoveTargetUnitList;

			//meshRenderer = this.GetComponent<MeshRenderer>();
			//originMaterial = meshRenderer.material;
			SceneManager.instance.RegisterUnit(this);
			
			inited = true;

		}



		public virtual void SetFraction(GlobalDefine.Fraction toFraction)
		{
			if (toFraction == GlobalDefine.Fraction.One)
			{
				fraction = GlobalDefine.Fraction.One;
				oppositeFraction = GlobalDefine.Fraction.Two;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction1Tag;
				//attackRangeCollider.myfraction = GlobalDefine.Fraction.One;
			}
			else if (toFraction == GlobalDefine.Fraction.Two)
			{
				fraction = GlobalDefine.Fraction.Two;
				oppositeFraction = GlobalDefine.Fraction.One;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction2Tag;
				//attackRangeCollider.myfraction = GlobalDefine.Fraction.Two;
			}
			else
			{
				fraction = GlobalDefine.Fraction.Netrual;
				oppositeFraction = GlobalDefine.Fraction.Netrual;
				this.gameObject.tag = GlobalDefine.ObjectTag.Fraction0Tag;
				//attackRangeCollider.myfraction = GlobalDefine.Fraction.Netrual;
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
				/*
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

				}*/
				StartAttackClosestTarget();
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
					if (m_pursueTargetID < 0)
					{
						unitState = GlobalDefine.UnitState.Patrolling;
					}
					else
					{
						unitState = GlobalDefine.UnitState.PursuingTarget;
					}
				}
				catch
				{
					throw;
				}
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
						Vector3 pos = TargetUnitList [closestTargetNum].transform.position;
						//近程攻击不打子弹
						TargetUnitList[closestTargetNum].TakeNormalAttack(soldierType.NormalAttackPower,soldierType.MakePikeEffect);
						pos.z = -1;
						GetComponent<LineRenderer> ().enabled = true;
						GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, pos });
						attackFrames = 20;
						InAudio.PostEvent(gameObject, SceneManager.instance.KnightAttackEvent);
					}

					if (soldierType.AttackType == 2)
					{
						Vector3 pos = TargetUnitList [closestTargetNum].transform.position;
						//远程攻击要打子弹
						TargetUnitList[closestTargetNum].TakeNormalAttack(soldierType.NormalAttackPower);

						pos.z = -1;
						GetComponent<LineRenderer> ().enabled = true;
						GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, pos });
						attackFrames = 20;
						InAudio.PostEvent(gameObject, SceneManager.instance.ArcherAttackEvent);
					}
				}

				//Debug.Log ("AttackOnce: " + soldierType.NormalAttackPower);
				yield return new WaitForSeconds(attackTime);
			}
		}



		public virtual void TakeNormalAttack(int hpAttack,bool attackpikeeffect=false)
		{
			if (hpAttack > soldierType.ArmorAgainstNormalAttack)
			{
				normalHP -= hpAttack - soldierType.ArmorAgainstNormalAttack;
			}

			if (attackpikeeffect && soldierType.BePikeAffected)
			{
				bUnderPikeEffect = true;
				lastPikedTime = Time.time;
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

			//Destroy(this.gameObject);
			SelfDestroy();
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

		protected virtual void FixedUpdate()
		{
			FixedUpdateMove();
			if (attackFrames == 0) {
				if (!isBeingDragged) {
					GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, transform.position }); // Resets line to 0
					GetComponent<LineRenderer> ().enabled = false;
					attackFrames--;
				}
			} else {
				attackFrames--;
			}
		}

		protected virtual void FixedUpdateMove()
		{
			if (unitState == GlobalDefine.UnitState.Standing)
			{
				//m_patrollingdestination=m_rigidbody2D.position;
				return;
			}
			if (m_rigidbody2D == null) return;
			
			Vector3 targetPosition=new Vector3();

			if (unitState == GlobalDefine.UnitState.Patrolling)
			{
				targetPosition = m_patrollingdestination;
			}
			else if (unitState == GlobalDefine.UnitState.PursuingTarget)
			{
				targetPosition = SceneManager.instance.QueryUnitPosition(m_pursueTargetID);
				if (targetPosition == Vector3.negativeInfinity)
				{
					unitState = GlobalDefine.UnitState.Standing;
					targetPosition = transform.position;
					Debug.LogWarning("失去目标id: "+m_pursueTargetID);
				}
			}


				Vector3 pos = m_rigidbody2D.position;

			if (bUnderPikeEffect&&soldierType.BePikeAffected)
			{
				if (Time.time - lastPikedTime > PikeRecoverTime)
				{
					bUnderPikeEffect = false;
				}
			}

			float speed = UnitSpeed * Time.fixedDeltaTime;
			if (bUnderPikeEffect)
			{
				speed = speed * pikedSpeedModifier;
			}

			pos = Vector3.MoveTowards(pos, targetPosition, speed);
				if ((pos - targetPosition).sqrMagnitude < 0.1f)
				{
					unitState = GlobalDefine.UnitState.Standing;
				}
				else
				{
					Vector3 vectorToTarget = targetPosition - transform.position;

					//vectorToTarget.Normalize();

					float rot_z = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
					//transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


					//m_rigidbody2D.MoveRotation(Mathf.LerpAngle(m_rigidbody2D.rotation,rot_z - 90, RotationSpeed * Time.deltaTime));
					//m_turnableRoot.transform.eulerAngles=new Vector3(0,0,Mathf.LerpAngle(m_turnableRoot.transform.eulerAngles.z,rot_z - 90, RotationSpeed * Time.deltaTime));

					//Quaternion newRot = Quaternion.Euler(new Vector3(0,0,rot_z - 90));
					m_turnableRoot.eulerAngles = new Vector3(0, 0, rot_z - 90);

					//m_turnableRoot.localRotation = Quaternion.Slerp(m_turnableRoot.localRotation, Quaternion.LookRotation(vectorToTarget, Vector3.up), RotationSpeed * Time.fixedDeltaTime);
				}
				//m_rigidbody2D.rotation = Quaternion.Slerp(m_rigidbody2D.rotation, Quaternion.LookRotation(target - pos, Vector3.up), UnitSpeed * Time.fixedDeltaTime);
			//if (m_rigidbody2D == null) return;
				m_rigidbody2D.position = pos;
		}

		protected virtual void Update()
		{
			MouseLogic();
		}

		protected void MouseLogic()
		{
			// Mouse logic
			if (mouseMode) {
				if (Input.GetMouseButtonDown(0))
				{
				}


				if (isBeingDragged) {
					if (m_currentArrow == null)
					{
						m_currentArrow = Instantiate (SceneManager.instance.arrowPrefab) as GameObject;
					}
					TouchDrag (Input.mousePosition);
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
							TouchDrag (Input.touches [i].position);
							break;
						} else if (i == Input.touches.Length - 1) {
							// Dragging has ended
							MovementDecided();
						}
					}
				}
			}
		}
		
		public void Grab()
		{
			if (!isDraggable) return;
			if (!isBeingDragged) {
				isBeingDragged = true;
				if (m_currentArrow == null)
				{
					m_currentArrow = Instantiate (SceneManager.instance.arrowPrefab) as GameObject;
				}
				GetComponent<LineRenderer> ().enabled = true;
			}
		}

		public void Grab(int touchFID) {
			if (!isBeingDragged) {
				touchFingerId = touchFID;
				Grab ();
			}
		}

		public void MovementDecided() {
			isBeingDragged = false;
			Destroy (m_currentArrow);
			GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, transform.position }); // Resets line to 0
			GetComponent<LineRenderer> ().enabled = false;
			StartCommanding(m_commanddestination);
			// At this point we need to generate a messenger from the general
		}

		public void TouchDrag(Vector3 pos) {
			Vector3 arrowPos = Camera.main.ScreenToWorldPoint (pos);
			arrowPos.z = -1f;
			m_currentArrow.transform.position = arrowPos;
			GetComponent<LineRenderer> ().SetPositions (new Vector3[] { transform.position, arrowPos });
			m_commanddestination = arrowPos;
		}


		public bool ReceiveCommand(int CommandID)
		{
			Command receivedCommand=CommandManager.instance.GetCommand(CommandID);
			return ReceiveCommand(receivedCommand);
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

			m_patrollingdestination = receivedCommand.m_commandTargetPostion;
			if (unitState == GlobalDefine.UnitState.Standing)
			{
				unitState = GlobalDefine.UnitState.Patrolling;
			}

			return true;
		}

		public void SelfDestroy()
		{
			SceneManager.instance.UnRegisterUnit(this.UnitID);
			Destroy(this.gameObject);
		}

		void StartCommanding(Vector2 destinationposition)
		{
			switch (soldierType.CommandType)
			{
				case	(int)GlobalDefine.CommandType.DirectControl:
					StartPatrol(destinationposition);
					break;
				case (int)GlobalDefine.CommandType.MessengerControl:
					StartIssueCommand(destinationposition);
					break;
				case (int)GlobalDefine.CommandType.Unable:
					break;
			}
		}

		protected void StartPatrol(Vector2 destinationposition)
		{
			unitState = GlobalDefine.UnitState.Patrolling;
			m_patrollingdestination = destinationposition;
		}

		protected void StartPursueTarget(int targetID)
		{
			m_pursueTargetID = targetID;
			unitState = GlobalDefine.UnitState.PursuingTarget;
		}

		public void StartIssueCommand(Vector2 destinationposition)
		{
			SceneManager.instance.FindGeneral(fraction).IssueCommand(fraction, UnitID, destinationposition);
		}

	}
}
