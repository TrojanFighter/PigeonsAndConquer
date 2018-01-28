using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSelfDestory : MonoBehaviour {

	public float m_selfDestoryTime = 1;
	protected virtual void Start()
	{
			StartCoroutine(SelfDestoryInTime(m_selfDestoryTime));
	}

	protected virtual IEnumerator SelfDestoryInTime(float timeleft)
	{
		yield return new WaitForSeconds(timeleft);
		//PoolBoss.SpawnOutsidePool(ProjectilePrefab.transform, spawnPos, m_turnableRoot.transform.rotation); 
		SelfDestoryNow();
	}

	protected virtual void SelfDestoryNow()
	{
		if (gameObject == null) return;
		//PoolBoss.Despawn(transform, false); 

		Destroy(gameObject);
	}
}
