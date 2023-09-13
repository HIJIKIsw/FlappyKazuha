using UnityEngine;

namespace Flappy.Gimmicks
{
    /// <summary>
	/// 地面
	/// </summary>
	public class Ground : MonoBehaviour
	{
		/// <summary>
		/// 地面を消す処理
		/// </summary>
		public void Remove()
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
