using UnityEngine;
using Flappy.Manager;

namespace Flappy.Items
{
	/// <summary>
	/// 原石
	/// </summary>
	public class Primogem : MonoBehaviour
	{
		/// <summary>
		/// 他のトリガコライダと接触した
		/// </summary>
		/// <param name="collider">接触対象のコライダ</param>
		void OnTriggerEnter2D(Collider2D collider)
		{
			// TODO: コライダからレイヤーマスクの名前を取得することが多そうなので、拡張クラスかユーティリティクラスにしてメソッド抽出する
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "Player":
					{
						GameManager.Instance.PrimogemCount++;
						GameObject.Destroy(this.gameObject);
						break;
					}
				default:
					{
						// Nothing to do.
						break;
					}
			}
		}
	}
}

