using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	public class PillarDestroyer : MonoBehaviour
	{
		/// <summary>
		/// 柱を消すための当たり判定
		/// </summary>
		void OnTriggerEnter2D(Collider2D collider)
		{
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "Pillar":
					{
						var pillarComponent = collider.gameObject.GetComponent<Pillar>();
						pillarComponent.Remove();
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