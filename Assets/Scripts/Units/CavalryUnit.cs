using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{

	public class CavalryUnit : Unit
	{

		public override void Init(){

			unitClass = GlobalDefine.UnitClass.Cavalry;
			base.Init ();
		}
	}
}
