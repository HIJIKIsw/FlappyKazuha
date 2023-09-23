using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	public class HeightLimit : MonoBehaviour
	{
		[SerializeField]
		PlayerBase player;
		[SerializeField]
		BoxCollider2D limiterCollider;
		// コライダーにコライダーがぶつかった時に始める処理
		void OnTriggerEnter2D(Collider2D collider)
		{
			// コライダーからゲームオブジェクトを経由してレイヤーを見に行く
			var otherLayer = collider.gameObject.layer;
			// ぶつかった相手のレイヤーの名前を取得
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "Player":
					{
						// transform.positionにはVector2か3のインスタンスしか挿入できないので、positionの新たな値を設定するためのVector2インスタンスをまず作成する
						Vector2 newPosition = new Vector2(player.transform.position.x, this.transform.position.y - limiterCollider.size.y);
						player.SetPosition(newPosition);
						break;
					}
				default:
					{
						break;
					}
			}
		}
	}
}