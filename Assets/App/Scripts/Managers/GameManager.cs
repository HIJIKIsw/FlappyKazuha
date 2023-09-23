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
		/// <remarks>値は常に少数第一位までで丸めて出し入れされる (四捨五入)</remarks>
		public float BestScore
		{
			get
			{
				return this.RoundScore(this.bestScore);
			}
			set
			{
				this.bestScore = this.RoundScore(value);
			}
		}
		private float bestScore = 0f;

		/// <summary>
		/// スコアを処理で扱える値に丸める
		/// スコアを比較したり保存したりする時は必ずこの処理を通すこと
		/// </summary>
		/// <param name="score">スコア</param>
		/// <remarks>少数第二位以下が四捨五入される</remarks>
		public float RoundScore(float score)
		{
			return (float)Math.Round(score, 1);
		}
	}
}

