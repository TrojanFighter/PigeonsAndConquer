using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

namespace Lords
{
	public class BasePoint : MonoBehaviour
	{
		/*
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}*/
		public GlobalDefine.Fraction baseFraction;
		Fraction fraction;
		public int FractionHP;
		float HumanResourceAddition = 0f;
		public GameObject FractionGod;
		public bool GodRaiseDirection, GodAdjustable = true, LookAtGodOne = false, LookAtGodTwo = false;
		Vector3 FractionGodBasePosition;

		void Awake()
		{
			fraction = DataManager.Fractions[(int) baseFraction];
			Init();
			FractionGodBasePosition = FractionGod.transform.position;
			DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
		}


		void Init()
		{
			FractionHP = fraction.FractionsInitHP;
		}
/*
		void Check()
		{


			if (FractionHP < 0)
			{
				FractionHP = 0;
				LoseHP();
			}

			if (FractionHP > fraction.FractionsMaxHP)
			{
				FractionHP = fraction.FractionsMaxHP;
			}

		}

		
		void LoseHP()
		{
			if (fraction.WinConditionHP)
			{
				Debug.Log(baseFraction + "LoseHP");
				GameManager.BasePoints[baseFraction].FractionGod.GetComponent<Rigidbody>().useGravity = true;
				GameManager.BasePoints[baseFraction].GodAdjustable = false;
				if (baseFraction == GlobalDefine.Fraction.Two)
				{
					GameManager.Instance.LeftPanel.GetComponentInChildren<Text>().text = "Victory";
					GameManager.Instance.RightPanel.GetComponentInChildren<Text>().text = "Lose";
					GameManager.BasePoints[GlobalDefine.Fraction.One].GodAdjustable = false;
					LookAtGodOne = true;
				}
				else if (baseFraction == GlobalDefine.Fraction.One)
				{
					GameManager.Instance.LeftPanel.GetComponentInChildren<Text>().text = "Lose";
					GameManager.Instance.RightPanel.GetComponentInChildren<Text>().text = "Victory";
					GameManager.BasePoints[GlobalDefine.Fraction.Two].GodAdjustable = false;
					LookAtGodTwo = true;
				}

				InAudio.PostEvent(gameObject, GameManager.Instance.VictoryBlast);
				GameManager.Instance.LeftPanel.SetTrigger("Result");
				GameManager.Instance.RightPanel.SetTrigger("Result");

				//GameManager.BasePoints [baseFraction].FractionGod.transform.DOLocalMoveY (200, 2f);
			}
		}


		void FixedUpdate()
		{

			if (LookAtGodOne)
			{
				Camera.main.transform.LookAt(GameManager.BasePoints[GlobalDefine.Fraction.One].FractionGod.transform);
				GameManager.BasePoints[GlobalDefine.Fraction.One].FractionGod.transform.position +=
					new Vector3(0, 30f * Time.fixedDeltaTime, 0);
			}
			else if (LookAtGodTwo)
			{
				Camera.main.transform.LookAt(GameManager.BasePoints[GlobalDefine.Fraction.Two].FractionGod.transform);
				GameManager.BasePoints[GlobalDefine.Fraction.Two].FractionGod.transform.position +=
					new Vector3(0, 30f * Time.fixedDeltaTime, 0);
			}
		}

		public void ChangeBaseValue(int HP, int MP, int HumanResource)
		{
			FractionHP += HP;
			//FractionGodAdjust (MP);
			Check();
		}*/



		/*
		void OnTriggerEnter (Collider other)
		{
			//other.GetComponent<Unit>()&&&&other.GetComponent<Unit>().fraction != m_fraction&&other.GetComponent<Unit>().unitState==GlobalDefine.UnitState.Patrolling
			if (other.GetComponent<Unit>()&&other.GetComponent<Unit>().fraction != baseFraction&&other.GetComponent<Unit>().unitState==GlobalDefine.UnitState.Patrolling) {
				Destroy (other.gameObject);
			}
	
			if (other.GetComponent<Unit>()!=null) {
				Destroy (other.gameObject);
			}
		}*/
	}
}
