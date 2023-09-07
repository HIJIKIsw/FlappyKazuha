using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	public class GroundDestroyer : MonoBehaviour
	{
		[SerializeField]
		BoxCollider2D collider;

		/// <summary>
		/// Groundを消すための当たり判定
		/// </summary>
		void OnTriggerEnter2D(Collider2D collider)
		{
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "Ground":
					{
						var GroundComponent = collider.gameObject.GetComponent<Ground>();
						GroundComponent.Remove();
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