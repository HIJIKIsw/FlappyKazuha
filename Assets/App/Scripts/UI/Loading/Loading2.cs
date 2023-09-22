using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Flappy.Common;

namespace Flappy.UI
{
	/// <summary>
	/// フルスクリーンロード画面
	/// </summary>
	public sealed class Loading2 : FullscreenLoadingBase
	{
		/// <summary>
		/// プログレスバーのImageオブジェクト
		/// </summary>
		[SerializeField]
		Image progressBarFill;

		protected override float fadeTime => 0.4f;
		protected override float minDisplayTime => 1.5f;

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		public override void SetProgress(float progress)
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
	}
}