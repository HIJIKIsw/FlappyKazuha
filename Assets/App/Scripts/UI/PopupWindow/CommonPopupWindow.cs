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
		CanvasGroup canvasGroup;

		/// <summary>
		/// ボタン
		/// </summary>
		[SerializeField]
		Button[] buttons;

		/// <summary>
		/// ボタンテキスト
		/// </summary>
		[SerializeField]
		TextMeshProUGUI[] buttonTexts;

		/// <summary>
		/// タイトル
		/// </summary>
		[SerializeField]
		TextMeshProUGUI title;

		/// <summary>
		/// メッセージ
		/// </summary>
		[SerializeField]
		TextMeshProUGUI message;

		/// <summary>
		/// SetButton~ 系で対象のボタン指定に使う
		/// </summary>
		public enum Buttons
		{
			Primary = 0,
			Secondary,
			Tertiary
		}

		/// <summary>
		/// 指定したボタンが見つからなかった場合のエラーメッセージ
		/// </summary>
		private const string missingButtonError = "指定されたボタンが見つかりませんでした。Inspector で対象のボタンへの参照が設定されているか確認してください。";

		/// <summary>
		/// フェードイン/フェードアウト時間
		/// </summary>
		private const float defaultFadeTime = 0.2f;

		/// <summary>
		/// インスタンス化しただけでは表示されないように非アクティブにする
		/// </summary>
		private void Awake()
		{
			this.gameObject.SetActive(false);
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
				GameObject.Destroy(this);
			}
		}

		/// <summary>
		/// タイトル文言をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetTitle(string title)
		{
			this.title.text = title;
			return this;
		}

		/// <summary>
		/// メッセージ文言をセット
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetMessage(string message)
		{
			this.message.text = message;
			return this;
		}

		/// <summary>
		/// ボタンの文言をセット
		/// </summary>
		/// <param name="target">対象のボタン</param>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetButtonText(Buttons target, string text)
		{
			// 指定されたボタンが見つからなかったらエラー
			var index = (int)target;
			if (index >= this.buttonTexts.Length)
			{
				Debug.LogAssertion(CommonPopupWindow.missingButtonError);
				return this;
			}

			// 文言をセット
			this.buttonTexts[index].text = text;
			return this;
		}

		/// <summary>
		/// ボタンの状態をセット
		/// </summary>
		/// <param name="target">対象のボタン</param>
		/// <param name="isVisible">ボタンを表示するか</param>
		/// <param name="isIntaractable">ボタンを押下可能にするか</param>
		/// <remarks>メソッドチェーン可</remarks>
		public CommonPopupWindow SetButtonState(Buttons target, bool isVisible, bool isIntaractable = true)
		{
			// 指定されたボタンが見つからなかったらエラー
			var index = (int)target;
			if (index >= this.buttonTexts.Length)
			{
				Debug.LogAssertion(CommonPopupWindow.missingButtonError);
				return this;
			}

			// 表示・非表示
			this.buttons[index].gameObject.SetActive(isVisible);
			// 押下可能・不可能
			this.buttons[index].interactable = isIntaractable;
			return this;
		}

		/// <summary>
		/// ボタンクリック時のアクションを設定
		/// </summary>
		/// <param name="target">対象のボタン</param>
		/// <param name="onClick">クリック時のアクション</param>
		/// <returns></returns>
		public CommonPopupWindow SetButtonClickEvent(Buttons target, UnityAction onClick)
		{
			// 指定されたボタンが見つからなかったらエラー
			var index = (int)target;
			if (index >= this.buttonTexts.Length)
			{
				Debug.LogAssertion(CommonPopupWindow.missingButtonError);
				return this;
			}

			// 既存のイベントを削除
			this.buttons[index].onClick.RemoveAllListeners();
			// イベントを登録
			this.buttons[index].onClick.AddListener(onClick);
			return this;
		}
	}
}