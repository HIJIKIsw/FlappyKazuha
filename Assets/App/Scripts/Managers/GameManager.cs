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
		// TODO: メンバー変数の量が増えてきたらUserInfoみたいなものを作ってそっちにまとめる。
		//       もしくはAPIのプロキシクラスを持たせたらメンバー自体が必要ない？
		/// <summary>
		/// 累計スコア
		/// </summary>
		public float AccumulatedScore
		{
			get
			{
				return this.RoundScore(this.accumulatedScore);
			}
			set
			{
				this.accumulatedScore = this.RoundScore(value);
			}
		}
		private float accumulatedScore;

		/// <summary>
		/// 累計原石
		/// </summary>
		public int AccumulatedGemScore { get; set; }

		/// <summary>
		/// ユーザID
		/// </summary>
		public int UserId { get; set; }

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
		/// TODO: そもそもこれはゲームプレイ中のカウントなのでPlayGameScene側に持たせる
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
		/// 最高スコアが記録されているか
		/// </summary>
		public bool IsRecordedBestScore
		{
			get
			{
				// MEMO: 条件を0.1以上としているのは、floatの丸め誤差による誤判定を防ぐため。0.1m以内の記録が残る想定がないためこれで必要十分。
				return this.BestScore >= 0.1f;
			}
		}

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

		/// <summary>
		/// スコアを画面に表示する形式の文字列に変換
		/// </summary>
		/// TODO: 丸め済みのスコアを渡した場合は改めて丸めないように第2引数で指定できるようにする。第2引数省略時は丸める。
		public string ScoreToText(float score)
		{
			var roundScore = GameManager.Instance.RoundScore(score);
			return roundScore.ToString("F1") + " m";
		}
	}
}

