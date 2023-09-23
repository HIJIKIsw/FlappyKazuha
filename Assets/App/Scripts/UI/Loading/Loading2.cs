using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Flappy.Common;
using Flappy.Manager;

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
		/// 初期化
		/// </summary>
		private void Start()
		{
			// 効果音を再生
			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku1, 0.3f, 0.8f);
			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku2, 0.7f, 2.5f);
		}

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		/// TODO: バーを巻き戻すようなことができないようにする
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