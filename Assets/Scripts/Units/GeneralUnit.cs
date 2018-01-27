using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

	private void FixedUpdate()
	{
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

	public void IssueCommand()
	{
		
	}
}
}