using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 柱
	/// </summary>
	public class Pillar : MonoBehaviour
	{
		/// <summary>
		/// 他のトリガコライダと接触した
		/// </summary>
		/// <param name="collider">接触対象のコライダ</param>
		/// TODO: PillarDestroyer側に移動する。こっちにはDestroyメソッドを実装する。
		void OnTriggerEnter2D(Collider2D collider)
		{
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "PillarDestroyer":
					{
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