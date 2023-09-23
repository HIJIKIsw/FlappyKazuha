using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Flappy.Common;

namespace Flappy.UI
{
	/// <summary>
	/// フルスクリーンロード画面 (プログレスバーなし)
	/// </summary>
	public sealed class Loading3 : FullscreenLoadingBase
	{
		/// <summary>
		/// Animator コンポーネント
		/// </summary>
		[SerializeField]
		Animator animator;

		/// <summary>
		/// 元素の種類
		/// </summary>
		/// TODO: 別の箇所で定数を持つようになったらそれに置き換える
		const int elementsCount = 7;

		protected override float fadeTime => 0.4f;
		protected override float minDisplayTime => 0.4f;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// 1元素の表示時間(割合)を取得
			var elementDisplayTime = 1f / Loading3.elementsCount;

			// アニメーションをランダムな位置から再生する
			var animationStartTime = elementDisplayTime * Random.Range(0, elementsCount);
			animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, animationStartTime);
		}
	}
}