using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPP
{

	public class PlayerPoweredUp : GameEvent
	{
		public GameObject playerGO;

		public PlayerPoweredUp(GameObject player)
		{
			playerGO = player;
		}
	}

	public class PlayerBehavior : MonoBehaviour
	{
		public string Name = "Player";

		void Start()
		{
			StartCoroutine(PowerUp());
		}

		IEnumerator PowerUp()
		{
			yield return  new WaitForSeconds(1);
			EventManager.Instance.Fire(new PlayerPoweredUp(gameObject));
		}
	}
}