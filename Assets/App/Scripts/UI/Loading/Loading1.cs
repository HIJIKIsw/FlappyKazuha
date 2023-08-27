using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.UI
{
	/// <summary>
	/// オーバーレイロード画面
	/// </summary>
	public class Loading1 : MonoBehaviour
	{
		/// <summary>
		/// ロード画面を表示する
		/// </summary>
		/// TODO: 必要に応じてコールバックデリゲートを実装する
		public void Show()
		{
			this.gameObject.SetActive(true);
		}

		/// <summary>
		/// ロード画面を非表示にする
		/// </summary>
		/// TODO: 必要に応じてコールバックデリゲートを実装する
		public void Hide()
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}