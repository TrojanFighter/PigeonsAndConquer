using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{

	public class ArcherUnit : Unit
	{

		public override void Init()
		{

			unitClass = GlobalDefine.UnitClass.Archer;
			base.Init();
		}
	}
}