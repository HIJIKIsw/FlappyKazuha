using Flappy.Api;
using Flappy.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Flappy.UI
{
	/// <summary>
	/// 名前変更ポップアップウィンドウ
	/// </summary>
	/// TODO: クラス構成ややこしくなりそうなら、CommonPopupWindowの親としてBaseクラスを用意してより汎用的なモデルにする
	public class ChangeNamePopupWindow : CommonPopupWindow
	{
		/// <summary>
		/// 入力欄に表示しているテキスト
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI nameText;

		/// <summary>
		/// 名前変更イベント通知デリゲート
		/// </summary>
		private UnityAction<string> onChangeName;

		/// <summary>
		/// 初期化処理
		/// </summary>
		protected new void Awake()
		{
			// インスタンス化しただけでは表示されないように非アクティブにする
			this.gameObject.SetActive(false);

			// ボタンがあるケースしかないためFooter:有効/EmptyFooter:無効にする
			this.footer.gameObject.SetActive(true);
			this.emptyFooter.gameObject.SetActive(false);
		}

		/// <summary>
		/// ウィンドウを表示する
		/// </summary>
		/// <param name="defaultNameText">初期値としてセットする名前</param>
		/// <param name="onChangeName">名前変更イベント通知デリゲート</param>
		public void Open(string defaultNameText = null, UnityAction<string> onChangeName = null)
		{
			// 基底クラスのOpenを呼ぶ
			this.Open(true);

			// 名前初期値をセット
			this.nameText.text = defaultNameText ?? string.Empty;

			// 名前変更イベントをセット
			this.onChangeName = onChangeName;
		}

		/// <summary>
		/// 使用できないメソッドのため呼ばれたらエラー出力して処理続行
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public new CommonPopupWindow SetMessage(string message)
		{
			Debug.LogAssertion("This method is not supported.");
			return this;
		}

		/// <summary>
		/// Okクリック時のアクション
		/// </summary>
		public void OnClickOk()
		{
			// 名前を変更していない場合は何もせず閉じる
			if (this.nameText.text == GameManager.Instance.UserName)
			{
				this.Close();
				return;
			}

			// TODO: バリデーション
			LoadingManager.Instance.Show();
			new UserInfoUpdateRequest(this.nameText.text).Request<UserInfoUpdateResponse>((response) =>
			{
				this.onChangeName?.Invoke(response.UserName);
				this.Close();
				LoadingManager.Instance.CompleteTask();
			});
		}
	}
}