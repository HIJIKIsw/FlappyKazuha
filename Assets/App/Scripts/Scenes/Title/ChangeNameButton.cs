using Flappy.UI;
using UnityEngine;

namespace Flappy.Title
{
	public class ChangeNameButton : MonoBehaviour
	{
		/// <summary>
		/// ポップアップウィンドウをインスタンス化する時の親オブジェクト
		/// </summary>
		[SerializeField]
		private GameObject popupRoot;

		/// <summary>
		/// 名前変更ポップアップウィンドウのプレハブ
		/// </summary>
		[SerializeField]
		private ChangeNamePopupWindow changeNamePopupPrefab;

		/// <summary>
		/// 名前変更ウィンドウを開く
		/// </summary>
		public void OpenChangeNameWindow()
		{
			// 名前変更ウィンドウプレハブをインスタンス化
			var window = GameObject.Instantiate(this.changeNamePopupPrefab, this.popupRoot.transform);
			window.Open();
		}
	}
}