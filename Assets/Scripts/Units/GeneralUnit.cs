using System.Collections;
using System.Collections.Generic;
using DarkTonic.CoreGameKit;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

namespace Lords
{
public class GeneralUnit : Unit
{
	// Pidgeon UI code
	public Sprite[] pidgeonUISprites;
	public Transform pidgeonUI;

	public int CurrentMessengerNum, MaxMessengerNum;

	public float MessengerRechargeRate = 0.2f, MessengerRechargePercentage = 0f;

	public int CurrentCannonNum = 1, MaxCannonNum = 1;
	public float CannonRechargeRate = 0.2f, CannonRechargePercentage = 0;
	public GameObject ProjectilePrefab;
	public string customEventName = "";
	public Image rechargeMeter;
	
		void Awake() {
			CurrentMessengerNum = 3;
			MaxMessengerNum = 3;
			base.Awake ();
		}
	
	// Use this for initialization
	public override void Init()
	{
		pidgeonUI = transform.Find ("pidgeonUI");
		unitClass = GlobalDefine.UnitClass.General;
		base.Init();

	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		MessengerRechargePerDeltaTime();
		CannonRechargePerDeltaTime();
	}

	void MessengerRechargePerDeltaTime()
	{
		if (CurrentMessengerNum < MaxMessengerNum)
		{
			MessengerRechargePercentage += MessengerRechargeRate * Time.fixedDeltaTime;
			if (MessengerRechargePercentage >= 1f)
			{
				CurrentMessengerNum++;
				if (CurrentMessengerNum > pidgeonUISprites.Length)
				{
					pidgeonUI.GetComponent<SpriteRenderer>().sprite = pidgeonUISprites[pidgeonUISprites.Length-1];
				}
				else
				{
					pidgeonUI.GetComponent<SpriteRenderer>().sprite = pidgeonUISprites[CurrentMessengerNum];
				}

				MessengerRechargePercentage = 0f;
			}
		}
	}
	void CannonRechargePerDeltaTime()
	{
		if (CurrentCannonNum < MaxCannonNum)
		{
			CannonRechargePercentage += CannonRechargeRate * Time.fixedDeltaTime;

			if (rechargeMeter != null)
			{
				rechargeMeter.fillAmount = CannonRechargePercentage;
			}

			if (CannonRechargePercentage >= 1f)
			{
				CurrentCannonNum++;
				CannonRechargePercentage = 1f;
			}
		}
	}

	public void ShootCannonForward()
	{
		if (CurrentCannonNum <= 0) return;
		var spawnPos = transform.position+m_turnableRoot.up*1;
		/*
		if (!string.IsNullOrEmpty(customEventName) && LevelSettings.CustomEventExists(customEventName)) {
			LevelSettings.FireCustomEvent(customEventName, transform);
		}*/
		//InAudio.PostEvent();
		//PoolBoss.SpawnOutsidePool(ProjectilePrefab.transform, spawnPos, m_turnableRoot.transform.rotation);
		GameObject newProjectile= Instantiate(ProjectilePrefab, spawnPos, m_turnableRoot.transform.rotation);
		newProjectile.GetComponent<Projectile>().FiringUnit = this;
		CurrentCannonNum--;
		CannonRechargePercentage = 0f;
	}

	public bool IssueCommand(GlobalDefine.Fraction fraction, int TargetUnitID, Vector2 destinationPosition)
	{
		if (CurrentMessengerNum <= 0)
		{
			return false;
		}
		else
		{
			Command command=new Command();
			command.m_commandTargetPostion = destinationPosition;
			command.m_fraction = fraction;
			command.m_TargetUnitID = TargetUnitID;
			int commandID= CommandManager.instance.GenerateCommand(command);
			SendMessenger(commandID);
			CurrentMessengerNum--;
			pidgeonUI.GetComponent<SpriteRenderer> ().sprite = pidgeonUISprites [CurrentMessengerNum];
			return true;
		}

	}

	void SendMessenger(int commandID)
	{
		if (gameObject == null)
		{return;
		}

		GameObject messenger;
		if (fraction == GlobalDefine.Fraction.One)
		{
			messenger = Instantiate(SceneManager.instance.Messenger1);
		}
		else
		{
			messenger = Instantiate(SceneManager.instance.Messenger2);
		}

		MessengerUnit messengerUnit= messenger.GetComponent<MessengerUnit>();
		if (messengerUnit == null)
		{
			Debug.LogError("Messenger Not Found");
			return;
		}

		messengerUnit.transform.position = transform.position;
		messengerUnit.Init();
		messengerUnit.CarryCommand(commandID);
		
	}
}
}