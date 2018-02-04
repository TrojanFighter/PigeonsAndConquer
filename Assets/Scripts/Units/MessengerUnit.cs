using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;

namespace Lords
{
	public class MessengerUnit : Unit
	{

		//public Command m_Command;
		public int m_commandID,m_commandTargetUnitID;
		public override void Init()
		{

			unitClass = GlobalDefine.UnitClass.Messenger;
			base.Init();
		}
		
		public void CarryCommand(int commandID)
		{
			Command m_command= CommandManager.instance.GetCommand(commandID);
			m_commandID = commandID;
			CarryCommand(m_command);
		}

		public void CarryCommand(Command inputCommand)
		{
			m_commandID=inputCommand.CommandID;
			m_commandTargetUnitID = inputCommand.m_TargetUnitID;
			StartPursueTarget(inputCommand.m_TargetUnitID);
		}

		public int CheckCommandTargetID()
		{
			return m_commandTargetUnitID;
		}

		public Command ExtractCommand()
		{
			return CommandManager.instance.GetCommand(m_commandID);
		}
		protected override void PlayAttackSoundOnce()
		{
			//InAudio.PostEvent(gameObject, SceneManager.instance.MessengerDeliverEvent);
		}
	}
}