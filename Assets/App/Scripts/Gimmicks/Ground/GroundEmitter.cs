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
		Ground groundPrefab;

		[SerializeField]
		float groundSpeed;

		[SerializeField]
		Vector2 startupGroundPosition;

		[SerializeField]
		GameObject groundContainer;

		//メンバー変数・最後に生成した地面オブジェクトを保持する　これは一つしかない
		Ground latestGround;

		const float emittingOffsetX = 1488;

		void Update()
		{	
			if ( this.latestGround == null || this.latestGround.transform.position.x <= 0 )
			{
				Vector2 groundBornPosition;
				if ( this.latestGround == null )
				{
					groundBornPosition = this.startupGroundPosition;
				}
				else
				{
					groundBornPosition = new Vector2(this.latestGround.transform.position.x+emittingOffsetX, this.latestGround.transform.position.y);
				}
				//オブジェクト生成時に「latestGround」変数に代入することにより、常に最後に生成した地面オブジェクトを参照できる
				this.latestGround = GameObject.Instantiate(this.groundPrefab, groundBornPosition, Quaternion.identity, groundContainer.transform);
				this.latestGround.SetSpeed(Vector2.left * this.groundSpeed);
			}
		}
	}
}
