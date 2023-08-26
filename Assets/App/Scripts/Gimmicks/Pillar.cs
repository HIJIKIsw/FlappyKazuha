using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 柱
	/// </summary>
	public class Pillar : MonoBehaviour
	{
		/// <summary>
		/// 柱を消す処理
		/// </summary>
		public void Remove()
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}