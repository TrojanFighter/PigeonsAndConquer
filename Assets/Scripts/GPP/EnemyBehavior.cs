using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPP
{




	public class EnemyBehavior : MonoBehaviour
	{

		// Use this for initialization
		void Start()
		{
			EventManager.Instance.Register<PlayerPoweredUp>(OnPlayerPoweredUp);
		}

		private void OnDestroy()
		{
			EventManager.Instance.UnRegister<PlayerPoweredUp>(OnPlayerPoweredUp);
		}

		void OnPlayerPoweredUp(GameEvent e)
		{
			var powerupEvent = e as PlayerPoweredUp;
			Debug.Log(powerupEvent.playerGO.name);
		}


	}
}
