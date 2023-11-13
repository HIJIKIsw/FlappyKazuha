using Flappy.Common;
using Flappy.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Flappy.UI
{
	/// <summary>
	/// 汎用ボタン
	/// </summary>
	/// TODO: もう少し基礎的な要素だけを抽出したCommonButtonクラスとIButtonWithLabel,IButtonWithIconなどのインターフェイスに分ける
	public class CommonButton : MonoBehaviour
	{
		/// <summary>
		/// アイコン画像
		/// </summary>
		[SerializeField]
		Image icon;

		/// <summary>
		/// ボタンの文言
		/// </summary>
		[SerializeField]
		TextMeshProUGUI label;

		/// <summary>
		/// ボタンコンポーネント
		/// </summary>
		[SerializeField]
		Button button;

		/// <summary>
		/// 初期化処理
		/// </summary>
		private void Start()
		{
			// アイコン画像がnullの場合はIconオブジェクトを無効にする
			this.icon.gameObject.SetActive(this.icon.sprite != null);
		}

		/// <summary>
		/// アイコン画像をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonButton SetIcon(Constants.Assets.Sprite.ButtonIcon icon)
		{
			// 意図しない画像が見えるのを防ぐため、アイコンが指定されたら読み込み完了まで一旦消す
			this.icon.gameObject.SetActive(false);

			// アイコンの画像アセットを取得
			var assetAddress = AssetAddressUtility.GetAssetAddress(icon);
			var asyncOperationHandle = Addressables.LoadAssetAsync<Sprite>(assetAddress);
			asyncOperationHandle.Completed += (handle) =>
			{
				// アイコンの画像をセット
				var sprite = handle.Result;
				this.icon.sprite = sprite;

				// アイコンを有効にする
				this.icon.gameObject.SetActive(true);
			};

			return this;
		}

		/// <summary>
		/// ボタンの文言をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonButton SetLabel(string label)
		{
			// ボタンの文言をセット
			this.label.text = label;

			return this;
		}

		/// <summary>
		/// クリック時のアクションをセット
		/// </summary>
		public CommonButton SetClickAction(UnityAction action)
		{
			// 既存のアクションを削除
			this.button.onClick.RemoveAllListeners();

			// アクションを登録
			this.button.onClick.AddListener(action);

			return this;
		}
	}
}