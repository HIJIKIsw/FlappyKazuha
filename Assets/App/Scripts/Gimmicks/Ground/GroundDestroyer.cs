using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
	public class GroundDestroyer : MonoBehaviour
	{
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
						var groundComponent = collider.gameObject.GetComponent<Ground>();
						groundComponent.Remove();
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