using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{
	public class PikemanUnit : Unit
	{

		public override void Init()
		{

			unitClass = GlobalDefine.UnitClass.Pikeman;
			base.Init();
		}
		protected override void PlayAttackSoundOnce()
		{
			InAudio.PostEvent(gameObject, SceneManager.instance.PickmanAttackEvent);
		}
	}
}