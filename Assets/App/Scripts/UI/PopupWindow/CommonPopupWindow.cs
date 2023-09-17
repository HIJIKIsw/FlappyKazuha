using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Flappy.UI
{
	/// <summary>
	/// 汎用ポップアップウィンドウクラス
	/// </summary>
	/// <remarks>必ず Open() を呼ぶこと。そうしないと表示されない。</remarks>
	public class CommonPopupWindow : MonoBehaviour
	{
		/// <summary>
		/// キャンバスグループ
		/// </summary>
		[SerializeField]
		protected CanvasGroup canvasGroup;

		/// <summary>
		/// WindowオブジェクトのRectTransform
		/// </summary>
		[SerializeField]
		protected RectTransform windowRect;

		/// <summary>
		/// タイトル
		/// </summary>
		[SerializeField]
		protected TextMeshProUGUI title;

		/// <summary>
		/// 閉じるボタンオブジェクト
		/// </summary>
		[SerializeField]
		protected GameObject closeButton;

		/// <summary>
		/// メッセージ
		/// </summary>
		[SerializeField]
		protected TextMeshProUGUI message;

		/// <summary>
		/// ボタングループオブジェクト
		/// </summary>
		[SerializeField]
		protected GameObject buttonGroup;

		/// <summary>
		/// Footerオブジェクト
		/// </summary>
		/// <remarks>ボタンを追加した場合こちらが有効になる</remarks>
		[SerializeField]
		protected GameObject footer;

		/// <summary>
		/// EmptyFooterオブジェクト
		/// </summary>
		/// <remarks>ボタンを追加しない場合こちらが有効になる</remarks>
		[SerializeField]
		protected GameObject emptyFooter;

		/// <summary>
		/// フェードイン/フェードアウト時間
		/// </summary>
		protected const float defaultFadeTime = 0.2f;

		/// <summary>
		/// ウィンドウが閉じられた時に実行されるアクション
		/// </summary>
		protected UnityAction onClose;

		/// <summary>
		/// 初期化処理
		/// </summary>
		protected void Awake()
		{
			// インスタンス化しただけでは表示されないように非アクティブにする
			this.gameObject.SetActive(false);

			// Footer オブジェクトを無効に、EmptyFooterを有効にする (デフォルトではボタンがないため)
			this.footer.gameObject.SetActive(false);
			this.emptyFooter.gameObject.SetActive(true);
		}

		/// <summary>
		/// ウィンドウを表示する
		/// </summary>
		/// <param name="isFadein">フェードインするか</param>
		public void Open(bool isFadein = true)
		{
			// ゲームオブジェクトをアクティブにする
			this.gameObject.SetActive(true);

			// フェードインする場合
			if (isFadein == true)
			{
				// フェードイン処理
				this.canvasGroup.alpha = 0f;
				this.canvasGroup.DOFade(1f, CommonPopupWindow.defaultFadeTime);
			}
			// フェードインしない場合
			else
			{
				// 即座に表示
				this.canvasGroup.alpha = 1f;
			}
		}

		/// <summary>
		///ウィンドウを閉じる
		/// </summary>
		/// <param name="isFadeout">フェードアウトするか</param>
		public void Close(bool isFadeout = true)
		{
			// onClose アクションが登録されていたら呼び出す
			this.onClose?.Invoke();

			// フェードアウトする場合
			if (isFadeout == true)
			{
				// フェードアウト処理
				this.canvasGroup.DOFade(0f, CommonPopupWindow.defaultFadeTime).OnComplete(() =>
				{
					this.Close(false);
				});
			}
			// フェードインしない場合
			else
			{
				// ゲームオブジェクトを削除
				GameObject.Destroy(this.gameObject);
			}
		}

		/// <summary>
		/// タイトル文言をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetTitle(string title)
		{
			// タイトル文言をセットする
			this.title.text = title;

			return this;
		}

		/// <summary>
		/// メッセージ文言をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetMessage(string message)
		{
			// メッセージ文言をセットする
			this.message.text = message;

			return this;
		}

		/// <summary>
		/// ボタンを追加
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow AddButton(CommonButton button)
		{
			// 引数で渡されたオブジェクトが不適切な場合
			if (button == null)
			{
				// エラーログを出力して処理終了
				Debug.LogAssertion("ボタンの追加に失敗しました。");
				return this;
			}

			// ボタングループの配下にボタンを配置する
			button.gameObject.transform.SetParent(this.buttonGroup.transform);

			// ボタンが1つでも追加されたら、Footer オブジェクトを有効に、EmptyFooterを無効にする
			this.footer.gameObject.SetActive(true);
			this.emptyFooter.gameObject.SetActive(false);

			return this;
		}

		/// <summary>
		/// ウィンドウサイズをセット
		/// </summary>
		/// <param name="size">ウィンドウのサイズ (px)</param>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetSize(Vector2 size)
		{
			// ウィンドウサイズをセット
			this.windowRect.sizeDelta = size;

			return this;
		}

		/// <summary>
		/// ウィンドウが閉じられた時のアクションをセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetOnCloseAction(UnityAction action)
		{
			// ウィンドウが閉じられた時のアクションをセット
			this.onClose = action;

			return this;
		}

		/// <summary>
		/// 閉じるボタンの有効/無効を切り替える
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetCloseButtonActive(bool value = true)
		{
			// 閉じるボタンの有効/無効を切り替える
			this.closeButton.SetActive(value);

			return this;
		}
	}
}