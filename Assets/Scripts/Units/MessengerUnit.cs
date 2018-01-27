using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;

namespace Lords
{
	public class MessengerUnit : Unit
	{

		//public Command m_Command;
		public int m_commandID,m_targetUnitID;
		public override void Init()
		{

			unitClass = GlobalDefine.UnitClass.Messenger;
			base.Init();
		}

		public void CarryCommand(Command inputCommand)
		{
			m_commandID=CommandManager.instance.GenerateCommand(inputCommand);
			m_targetUnitID = inputCommand.m_TargetUnitID;
		}

		public int CheckCommandTargetID()
		{
			return m_targetUnitID;
		}

		public Command ExtractCommand()
		{
			return CommandManager.instance.GetCommand(m_commandID);
		}
	}
}