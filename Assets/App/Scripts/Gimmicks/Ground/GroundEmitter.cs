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

		GameObject latestGround;

		float emittingPositionX = 1488;

        void Update()
		{
			
			if ( this.latestGround == null || this.latestGround.transform.position.x <= 0 )
			{
				Vector2 GroundBornX;
				if ( this.latestGround == null )
				{
					GroundBornX = this.StartupGroundPosition;
				}
				else
				{
					GroundBornX = new Vector2(this.latestGround.transform.position.x+emittingPositionX, this.latestGround.transform.position.y);
				}
				this.latestGround = GameObject.Instantiate(this.GroundPrefab, GroundBornX, Quaternion.identity);
				this.latestGround.GetComponent<Rigidbody2D>().velocity = Vector2.left * this.GroundSpeed;

            }
        }
    }
}
