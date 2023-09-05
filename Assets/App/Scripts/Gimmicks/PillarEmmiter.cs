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

		[SerializeField]
		GameObject primogemPrefab;

		[SerializeField]
		GameObject pillarContainer;

		void Update()
		{
			this.currentTime += Time.deltaTime;
			if( this.currentTime >= span )
			{
				this.currentTime = 0f;

				var randY = Random.Range(-pillarYRandomRange, pillarYRandomRange);
				var pillarPosition = this.transform.position + (Vector3.up * randY);
				
				// TODO: パフォーマンス改善のため、GetComponentではなくPillarクラス側にpublicなメンバを作ってそこから参照する
				var pillar = GameObject.Instantiate(this.pillarPrefab, pillarPosition, Quaternion.identity, pillarContainer.transform);
				pillar.GetComponent<Rigidbody2D>().velocity = Vector2.left * this.pillarSpeed;

				var primogem = GameObject.Instantiate(this.primogemPrefab, pillar.transform);
				primogem.transform.localPosition = Vector2.zero;
				
				// TODO: 柱のY座標から独立した乱数を生成して原石のY座標を設定する
				// TODO: Vector2やVector3のメンバに大して直接加算減算できる拡張メソッドを作る
				primogem.transform.localPosition = new Vector2(primogem.transform.localPosition.x, primogem.transform.localPosition.y + randY/4);
			}
		}
	}
}
