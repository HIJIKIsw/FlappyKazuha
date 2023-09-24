using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	public class HeightLimit : MonoBehaviour
	{
		/// <summary>
		/// 高度制限コライダ
		/// </summary>
		[SerializeField]
		BoxCollider2D limiterCollider;

		/// <summary>
		/// Playerインスタンス
		/// </summary>
		private PlayerBase playerInstance;

		/// <summary>
		/// コライダーにコライダーがぶつかった時に呼ばれる
		/// </summary>
		/// <param name="collider">接触相手のコライダ</param>
		void OnTriggerEnter2D(Collider2D collider)
		{
			// コライダーからゲームオブジェクトを経由してレイヤーを見に行く
			var otherLayer = collider.gameObject.layer;
			// ぶつかった相手のレイヤーの名前を取得
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			// 接触相手のコライダによって場合分け
			switch (otherLayerName)
			{
				case "Player":
					{
						// transform.positionにはVector2か3のインスタンスしか挿入できないので、positionの新たな値を設定するためのVector2インスタンスをまず作成する
						Vector2 newPosition = new Vector2(playerInstance.transform.position.x, this.transform.position.y - limiterCollider.size.y);
						playerInstance.SetPosition(newPosition);
						break;
					}
				default:
					{
						break;
					}
			}
		}

		/// <summary>
		/// Playerのインスタンスをセット
		/// </summary>
		/// <param name="instance">Playerのインスタンス</param>
		public void SetPlayerInstance(PlayerBase instance)
		{
			this.playerInstance = instance;
		}
	}
}