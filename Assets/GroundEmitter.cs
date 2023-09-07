using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Gimmicks
{
    /// <summary>
    /// 地面エミッター
    /// </summary>
    public class GroundEmmiter : MonoBehaviour
	{
    	[SerializeField]
		GameObject GroundPrefab;

		[SerializeField]
		float span;

		float currentTime = 0f;

		[SerializeField]
		float GroundSpeed;

        void Update()
		{
			this.currentTime += Time.deltaTime;
			if( this.currentTime >= span )
			{
				this.currentTime = 0f;

				var Ground = GameObject.Instantiate(this.GroundPrefab, GroundPosition, Quaternion.identity);
				Ground.GetComponent<Rigidbody2D>().velocity = Vector2.left * this.GroundSpeed;

            }
        }
    }
}
