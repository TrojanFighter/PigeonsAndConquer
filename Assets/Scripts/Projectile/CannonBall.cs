using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{

	public class CannonBall : Projectile
	{

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		/*protected override void OnTriggerEnter2D(Collider2D other)
		{

		}

		protected override void OnTriggerStay2D(Collider2D other)
		{

		}

		protected override void OnTriggerExit2D(Collider2D other)
		{

		}*/

		protected override void Impact()
		{
			base.Impact();
		}
	}
}
