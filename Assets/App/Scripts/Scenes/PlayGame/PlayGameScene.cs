using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Flappy.Common;
using Flappy.Gimmicks;
using Flappy.Manager;

namespace Flappy
{
	/// <summary>
	/// PlayGameシーン
	/// </summary>
	public class PlayGameScene : SceneBase
	{
		/// <summary>
		/// シーン名
		/// </summary>
		public override string Name => "PlayGame";

		/// <summary>
		/// PillartContainerオブジェクト
		/// </summary>
		[SerializeField]
		private GameObject pillarContainer;

		/// <summary>
		/// PillarEmitterオブジェクト
		/// </summary>
		[SerializeField]
		private PillarEmmiter pillarEmmiter;

		/// <summary>
		/// GroundContainerオブジェクト
		/// </summary>
		[SerializeField]
		private GameObject groundContainer;

		/// <summary>
		/// GroundEmitterオブジェクト
		/// </summary>
		[SerializeField]
		private GroundEmmiter groundEmmiter;

		/// <summary>
		/// CurrentScore -> Valueオブジェクト
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI currentScoreText;

		/// <summary>
		/// BestScore -> Valueオブジェクト
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI bestScoreText;

		/// <summary>
		/// スコア加算フラグ
		/// </summary>
		public bool IsProceedScoreCount { get; set; } = false;

		/// <summary>
		/// 現在スコア
		/// </summary>
		private float currentScore = 0f;

		/// <summary>
		/// 自己ベスト
		/// </summary>
		private float bestScore = 0f;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// TODO: タップでスタート実装後はタップするまでカウント始まらないようにする
			this.IsProceedScoreCount = true;

			this.bestScore = GameManager.Instance.BestScore;
			this.bestScoreText.text = this.scoreToText(this.bestScore);
		}

		/// <summary>
		/// 更新 (1フレーム)
		/// </summary>
		private void Update()
		{
			if (this.IsProceedScoreCount == true)
			{
				this.currentScore += Time.deltaTime * Constants.Game.ScorePerSecond;
			}
		}

		/// <summary>
		/// 更新 (固定時間)
		/// </summary>
		private void FixedUpdate()
		{
			currentScoreText.text = this.scoreToText(this.currentScore);
			if (Math.Round(this.currentScore, 1) > GameManager.Instance.BestScore)
			{
				this.bestScoreText.text = this.scoreToText(this.currentScore);
			}
		}

		/// <summary>
		/// ゲームオーバー時処理
		/// </summary>
		public void GameOver()
		{
			this.IsProceedScoreCount = false;

			// 自己ベストは少数第一位までで保持されているので丸めてから比較する
			if (Math.Round(this.currentScore, 1) > GameManager.Instance.BestScore)
			{
				GameManager.Instance.BestScore = this.currentScore;
			}

			// 全ての柱を停止させ、出現しないようにする
			var pillars = this.GetAllPillars();
			foreach (var pillar in pillars)
			{
				pillar.SetSpeed(Vector2.zero);
			}
			this.pillarEmmiter.gameObject.SetActive(false);

			// 全ての地面を停止させ、出現しないようにする
			var grounds = this.GetAllGrounds();
			foreach (var ground in grounds)
			{
				ground.SetSpeed(Vector2.zero);
			}
			this.groundEmmiter.gameObject.SetActive(false);

			// TODO: リザルト画面
			DOVirtual.DelayedCall(2f, () =>
			{
				SceneManager.Instance.Load<TitleScene>();
			});
		}

		/// <summary>
		/// 全ての柱を取得
		/// </summary>
		private List<Pillar> GetAllPillars()
		{
			var pillars = new List<Pillar>();
			var pillarCount = this.pillarContainer.transform.childCount;
			for (int i = 0; i < pillarCount; i++)
			{
				var pillar = this.pillarContainer.transform.GetChild(i).GetComponent<Pillar>();
				if (pillar != null)
				{
					pillars.Add(pillar);
				}
			}
			return pillars;
		}

		/// <summary>
		/// 全ての地面を取得
		/// </summary>
		private List<Ground> GetAllGrounds()
		{
			var grounds = new List<Ground>();
			var groundCount = this.groundContainer.transform.childCount;
			for (int i = 0; i < groundCount; i++)
			{
				var ground = this.groundContainer.transform.GetChild(i).GetComponent<Ground>();
				if (ground != null)
				{
					grounds.Add(ground);
				}
			}
			return grounds;
		}

		/// <summary>
		/// スコアを画面に表示する形式の文字列に変換
		/// </summary>
		private string scoreToText(float score)
		{
			var roundScore = Math.Round(score, 1);
			return roundScore.ToString("F1") + " m";
		}
	}
}