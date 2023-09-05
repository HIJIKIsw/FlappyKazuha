using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 柱
	/// </summary>
	public class Pillar : MonoBehaviour
	{
		[SerializeField]
		new Rigidbody2D rigidbody2D;

		/// <summary>
		/// 柱を消す処理
		/// </summary>
		public void Remove()
		{
			GameObject.Destroy(this.gameObject);
		}

		/// <summary>
		/// 移動速度を設定する
		/// </summary>
		public void SetSpeed(Vector2 speed)
		{
			this.rigidbody2D.velocity = speed;
		}
	}
}