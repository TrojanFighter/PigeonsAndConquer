using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.CoreGameKit;

namespace Lords
{



    public class Projectile : MonoBehaviour
    {

        //public Vector3 angles;
        public Unit FiringUnit;
        public float speed = 3f;
        public bool bSelfDestory = true, impactWhenSelfDestroy = true;
        public float m_selfDestoryTime = 2;

        public float explsionRadius = 2f;
        public int NormalAttackDamage = 20;

        protected virtual void FixedUpdate()
        {
            var pos = transform.position + transform.up * speed * Time.fixedDeltaTime;
            transform.position = pos;
        }

        protected virtual void Start()
        {
            if (bSelfDestory)
            {
                StartCoroutine(SelfDestoryInTime(m_selfDestoryTime));
            }
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
            if (impactWhenSelfDestroy)
            {
                Impact();
            }

            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.GetComponent<Unit>()){
                //dont hurt who fired this
            if (other.GetComponent<Unit>() == FiringUnit)
            {
                return;
            }
            else
            {
                Impact();
                SelfDestoryNow();
            }
            }

        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {

        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {

        }

        protected virtual void Impact()
        {
            //int layermask = LayerMask.NameToLayer("Units");
            Collider2D[] impacted= Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), explsionRadius);//,layermask);
            if (impacted == null || impacted.Length <= 0)
            {
            }
            else
            {
                for (int i = 0; i < impacted.Length; i++)
                {
                    if(impacted[i].GetComponent<Unit>())
                    impacted[i].GetComponent<Unit>().TakeNormalAttack(NormalAttackDamage);
                }
            }

            PlayExplosion();
        }

        protected virtual void PlayExplosion()
        {
        }
    }
}