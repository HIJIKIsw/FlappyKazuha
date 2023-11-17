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
		public void ShowErrorAndStopAllRequest()
		{
			var button = GameObject.Instantiate(this.buttonPrefab);
			button.SetIcon(Constants.Assets.Sprite.ButtonIcon.Circle)
			.SetLabel("OK")
			.SetClickAction(this.TransitionToTitle);

			var popup = GameObject.Instantiate(this.popupPrefab, this.transform);
			popup.SetTitle("通信エラー")
			.SetMessage("サーバーとの通信に失敗しました。タイトル画面に戻ります。")
			.SetCloseButtonActive(false)
			.AddButton(button);
			this.StopAllCoroutines();
		}

		/// <summary>
		/// タイトルシーンに遷移する
		/// </summary>
		private void TransitionToTitle()
		{
			SceneManager.Instance.Load<TitleScene>(null, LoadingManager.Types.FullscreenWithoutProgressbar);
		}
	}
}