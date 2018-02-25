using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPP;

namespace Lords
{

	public class CavalryUnit : Unit
	{

		public override void Init(){

			unitClass = GlobalDefine.UnitClass.Cavalry;
			base.Init ();
			EventManager.Instance.Register<OnSpeedUp>(OnUnitDieSpeedUp);
		}
		protected override void PlayAttackSoundOnce()
		{
			InAudio.PostEvent(gameObject, SceneManager.instance.CavalryAttackEvent);
		}
		
		void OnUnitDieSpeedUp(GameEvent e)
		{
			var speedUpEvent = e as OnSpeedUp;
			//Debug.Log(powerupEvent.playerGO.name);
			UnitSpeed += speedUpEvent.upSpeed;
			Debug.Log(UnitSpeed+" "+ speedUpEvent.upSpeed);

		}

		public override void SelfDestroy()
		{

			EventManager.Instance.UnRegister<OnSpeedUp>(OnUnitDieSpeedUp);
			base.SelfDestroy();
		}
	}
}
