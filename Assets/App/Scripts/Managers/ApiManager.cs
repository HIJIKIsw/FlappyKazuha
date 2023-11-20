using System;
using System.Collections;
using System.Collections.Generic;
using Flappy.Common;
using Flappy.UI;
using UnityEngine;
using UnityEngine.Internal;

namespace Flappy.Manager
{
	/// <summary>
	/// APIリクエストのコルーチンなどを管理する
	/// </summary>
	public class ApiManager : SingletonMonoBehaviour<ApiManager>
	{
		/// <summary>
		/// ポップアップウィンドウプレハブ
		/// </summary>
		[SerializeField]
		private CommonPopupWindow popupPrefab;

		/// <summary>
		/// ボタンプレハブ
		/// </summary>
		[SerializeField]
		private CommonButton buttonPrefab;

		/// <summary>
		/// コルーチンでリクエストを開始
		/// </summary>
		public void StartRequest(IEnumerator coroutine)
		{
			this.StartCoroutine(coroutine);
		}

		/// <summary>
		/// エラーを表示して全てのリクエストコルーチンを停止
		/// </summary>
		/// <param name="title">エラーポップアップに表示するタイトル</param>
		/// <param name="message">エラーポップアップに表示するメッセージ</param>
		public void ShowErrorAndStopAllRequest(string title = null, string message = null)
		{
			var button = GameObject.Instantiate(this.buttonPrefab);
			var popup = GameObject.Instantiate(this.popupPrefab, this.transform);

			button.SetIcon(Constants.Assets.Sprite.ButtonIcon.Circle)
			.SetLabel("タイトル画面に戻る")
			.SetClickAction(() =>
			{
				popup.Close();
				SceneManager.Instance.Load<TitleScene>(null, LoadingManager.Types.FullscreenWithoutProgressbar);
			});

			popup.SetTitle(title ?? "接続エラー")
			.SetMessage(message ?? "サーバー接続に失敗しました。\r\nサーバーが応答しませんでした。")
			.SetCloseButtonActive(false)
			.AddButton(button)
			.Open();

			this.StopAllCoroutines();
		}
	}
}