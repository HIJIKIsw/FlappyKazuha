using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Flappy.Gimmicks
{
    /// <summary>
    /// 地面エミッター
    /// </summary>
    public class GroundEmmiter : MonoBehaviour
	{
    	[SerializeField]
		GameObject GroundPrefab;

		[SerializeField]
		float GroundSpeed;

		[SerializeField]
		Vector2 StartupGroundPosition;

		[SerializeField]
		GameObject GroundContainer;

		GameObject latestGround;

		float emittingPositionX = 1488;

        void Update()
		{
			
			if ( this.latestGround == null || this.latestGround.transform.position.x <= 0 )
			{
				Vector2 groundBornPosition;
				if ( this.latestGround == null )
				{
					groundBornPosition = this.StartupGroundPosition;
				}
				else
				{
					groundBornPosition = new Vector2(this.latestGround.transform.position.x+emittingPositionX, this.latestGround.transform.position.y);
				}
				this.latestGround = GameObject.Instantiate(this.GroundPrefab, groundBornPosition, Quaternion.identity);
				this.latestGround.GetComponent<Rigidbody2D>().velocity = Vector2.left * this.GroundSpeed;

            }
        }
    }
}
