using System;
using Flappy.Common;
using UnityEngine;

namespace Flappy.Manager
{
	/// <summary>
	///  ゲームマネージャ
	/// </summary>
	public class GameManager : SingletonMonoBehaviour<GameManager>
	{
		/// <summary>
		/// コンストラクタを隠蔽する
		/// </summary>
		GameManager()
		{
			// Nothing to do.
		}

		/// <summary>
		/// 初期化
		/// </summary>
		void Start()
		{
			// マルチタッチを無効化
			Input.multiTouchEnabled = false;
		}

		/// <summary>
		/// 原石の所持数
		/// </summary>
		/// TODO: 所持品クラスみたいなものを作ってそっちにまとめる (原石、ネームプレート、称号など)
		public int PrimogemCount { get; set; } = 0;

		/// <summary>
		/// 自己ベストスコア
		/// </summary>
		/// <remarks>値は常に少数第一位までで出し入れされる (四捨五入)</remarks>
		public float BestScore
		{
			get
			{
				return (float)Math.Round(this.bestScore, 1);
			}
			set
			{
				this.bestScore = (float)Math.Round(value, 1);
			}
		}
		private float bestScore = 0f;
	}
}

