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
	public class PlayGameScene : SceneBase
	{
		/// <summary>
		/// シーン名
		/// </summary>
		public override string Name => "PlayGame";

		[SerializeField]
		private GameObject pillarContainer;

		[SerializeField]
		private PillarEmmiter pillarEmmiter;

		[SerializeField]
		private TextMeshProUGUI currentScoreText;

		[SerializeField]
		private TextMeshProUGUI bestScoreText;

		public bool IsProceedScoreCount { get; set; } = false;

		private float currentScore = 0f;
		private float bestScore = 0f;

		private void Start()
		{
			// TODO: タップでスタート実装後はタップするまでカウント始まらないようにする
			this.IsProceedScoreCount = true;

			this.bestScore = GameManager.Instance.BestScore;
			this.bestScoreText.text = this.scoreToText(this.bestScore);
		}

		private void Update()
		{
			if (this.IsProceedScoreCount == true)
			{
				this.currentScore += Time.deltaTime * Constants.Game.ScorePerSecond;
			}
		}

		private void FixedUpdate()
		{
			currentScoreText.text = this.scoreToText(this.currentScore);
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

			var pillars = this.GetAllPillars();
			foreach (var pillar in pillars)
			{
				pillar.SetSpeed(Vector2.zero);
			}
			this.pillarEmmiter.gameObject.SetActive(false);

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
		/// スコアを画面に表示する形式の文字列に変換
		/// </summary>
		private string scoreToText(float score)
		{
			var roundScore = Math.Round(score, 1);
			return roundScore.ToString("F1") + " m";
		}
	}
}