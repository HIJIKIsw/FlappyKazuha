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

		UnityAction onBeginLoad;
		UnityAction onCompleteLoad;

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
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.DOFade(0f, fadeTime).OnComplete(() =>
			{
				GameObject.Destroy(this.gameObject);
			});
		}

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		public void SetProgress(float progress)
		{
			progress = Mathf.Clamp(progress, 0f, 1f);
			this.progressBarFill.DOFillAmount(progress, 0.08f).OnComplete(() =>
			{
				if (progress >= 1f)
				{
					this.onCompleteLoad?.Invoke();
					this.Hide();
				}
			});
		}
	}
}