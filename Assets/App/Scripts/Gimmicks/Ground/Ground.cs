using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 地面
	/// </summary>
	public class Ground : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D rb2D;

		/// <summary>
		/// 地面を消す処理
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
			this.rb2D.velocity = speed;
		}
	}
}
