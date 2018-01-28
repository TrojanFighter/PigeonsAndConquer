﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
namespace Lords
{
public class GeneralUnit : Unit
{

	public int CurrentMessengerNum = 4, MaxMessengerNum = 4;

	public float MessengerRechargeRate = 0.2f, MessengerRechargePercentage = 0f;
	// Use this for initialization
	public override void Init()
	{

		unitClass = GlobalDefine.UnitClass.General;
		base.Init();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		MessengerRechargePerDeltaTime();
	}

	void MessengerRechargePerDeltaTime()
	{
		if (CurrentMessengerNum < MaxMessengerNum)
		{
			MessengerRechargePercentage += MessengerRechargeRate * Time.fixedDeltaTime;
			if (MessengerRechargePercentage >= 1f)
			{
				CurrentMessengerNum++;
				MessengerRechargePercentage = 0f;
			}
		}
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
			return true;
		}

	}

	public void SendMessenger(int commandID)
	{
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