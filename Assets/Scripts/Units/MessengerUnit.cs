using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;

namespace Lords
{
	public class MessengerUnit : Unit
	{

		public Command m_Command;
		public override void Init()
		{

			unitClass = GlobalDefine.UnitClass.Messenger;
			base.Init();
		}
	}
}