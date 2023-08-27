using DG.Tweening;
using UnityEngine;
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

		/// <summary>
		/// ロード画面を表示する
		/// </summary>
		/// <param name="fadeTime">フェードイン時間</param>
		/// TODO: 必要に応じてコールバックデリゲートを実装する
		public void Show(float fadeTime = Loading2.defaultFadeTime)
		{
			this.gameObject.SetActive(true);
			this.canvasGroup.alpha = 0f;
			this.canvasGroup.DOFade(1f, fadeTime);
		}

		/// <summary>
		/// ロード画面を非表示にする
		/// </summary>
		/// <param name="fadeTime">フェードアウト時間</param>
		/// TODO: 必要に応じてコールバックデリゲートを実装する
		public void Hide(float fadeTime = Loading2.defaultFadeTime)
		{
			this.canvasGroup.alpha = 1f;
			this.canvasGroup.DOFade(0f, fadeTime).OnComplete(() =>
			{
				this.gameObject.SetActive(false);
			});
		}

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		public void SetProgress(float progress)
		{
			progress = Mathf.Clamp(progress, 0f, 1f);
			this.progressBarFill.fillAmount = progress;
		}
	}
}