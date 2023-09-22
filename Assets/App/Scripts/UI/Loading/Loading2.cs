using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Flappy.UI
{
	/// <summary>
	/// フルスクリーンロード画面
	/// </summary>
	public class Loading2 : MonoBehaviour
	{
		[SerializeField]
		Image progressBarFill;

		[SerializeField]
		CanvasGroup canvasGroup;

		const float defaultFadeTime = 0.4f;
		const float minDisplayTime = 1.5f;

		float elapsedTime = 0f;
		Color32 loadingBgcolor;

		UnityAction onBeginLoad;
		UnityAction onCompleteLoad;

		private void Update()
		{
			this.elapsedTime += Time.deltaTime;

			/// <summary>
			/// 時間帯によって色を帰る
			/// </summary>
			
			//時
			int hour = System.DateTime.Now.Hour;
			if ( hour >= 7 & hour <= 19 )
			{
				this.loadingBgcolor = new Color32(255,255,255,255);
			}
			else
			{
				this.loadingBgcolor = new Color32(0,0,0,255);
			}
			

		}

		/// <summary>
		/// ロード画面を表示する
		/// </summary>
		/// <param name="fadeTime">フェードイン時間</param>
		/// <param name="onBeginLoad">ロード開始時(フェードイン完了後)に実行するアクション</param>
		/// <param name="onCompleteLoad">ロード完了時(フェードイン開始前)に実行するアクション</param>
		public void Show(float fadeTime = Loading2.defaultFadeTime, UnityAction onBeginLoad = null, UnityAction onCompleteLoad = null)
		{
			this.onBeginLoad = onBeginLoad;
			this.onCompleteLoad = onCompleteLoad;

			this.gameObject.SetActive(true);
			this.canvasGroup.alpha = 0f;
			this.canvasGroup.DOFade(1f, fadeTime).OnComplete(() =>
			{
				this.onBeginLoad?.Invoke();
			});
		}

		/// <summary>
		/// ロード画面を非表示にする
		/// </summary>
		/// <param name="fadeTime">フェードアウト時間</param>
		public void Hide(float fadeTime = Loading2.defaultFadeTime)
		{
			// 最低表示時間が経過するまで待機
			this.StartCoroutine(this.WaitForMinDisplayTime(() =>
			{
				this.onCompleteLoad?.Invoke();

				this.canvasGroup.alpha = 1f;
				this.canvasGroup.DOFade(0f, fadeTime).OnComplete(() =>
				{
					GameObject.Destroy(this.gameObject);
				});
			}));
		}

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		public void SetProgress(float progress)
		{
			progress = Mathf.Clamp(progress, 0f, 1f);
			this.progressBarFill.DOFillAmount(progress, 0.2f).OnComplete(() =>
			{
				if (progress >= 1f)
				{
					this.Hide();
				}
			});
		}

		/// <summary>
		/// 最低表示時間が経過するまで待機する
		/// </summary>
		/// <param name="completed">待機後に実行するアクション</param>
		private IEnumerator WaitForMinDisplayTime(UnityAction completed)
		{
			while (this.elapsedTime < Loading2.minDisplayTime)
			{
				yield return null;
			}
			completed?.Invoke();
		}
		
	}
}