using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 柱エミッター
	/// </summary>
	public class PillarEmmiter : MonoBehaviour
	{
		[SerializeField]
		GameObject pillarPrefab;

		[SerializeField]
		float span;

		float currentTime = 0f;

		[SerializeField]
		float pillarSpeed;

		[SerializeField]
		float pillarYRandomRange;

		void Update()
		{
			this.currentTime += Time.deltaTime;
			if( this.currentTime >= span )
			{
				this.currentTime = 0f;

				var randY = Random.Range(-pillarYRandomRange, pillarYRandomRange);
				var pillarPosition = this.transform.position + (Vector3.up * randY);
				
				// TODO: パフォーマンス改善のため、GetComponentではなくPillarクラス側にpublicなメンバを作ってそこから参照する
				var pillar = GameObject.Instantiate(this.pillarPrefab, pillarPosition, Quaternion.identity);
				pillar.GetComponent<Rigidbody2D>().velocity = Vector2.left * this.pillarSpeed;
			}
		}
	}
}
