using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Flappy.Common;
using Flappy.Gimmicks;
using Flappy.Manager;
using Flappy.UI;

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
		/// CommonPopupプレハブ
		/// </summary>
		[SerializeField]
		private CommonPopupWindow poopupWindowPrefab;

		/// <summary>
		/// CommonButtonプレハブ
		/// </summary>
		[SerializeField]
		private CommonButton buttonPrefab;

		/// <summary>
		/// スコア加算フラグ
		/// </summary>
		public bool IsProceedScoreCount { get; set; } = false;

		/// <summary>
		/// 現在スコア
		/// </summary>
		private float currentScore = 0f;

		/// <summary>
		/// 現在スコアが自己ベストを超えているか
		/// </summary>
		/// <remarks>
		/// 自己ベストを超えているかどうかの比較には必ずこのプロパティを使う
		/// 自己ベストは少数第一位までで保持されているので普通に比較すると正しい結果が得られないため
		/// </remarks>
		private bool isExceededBestScore
		{
			get
			{
				// 自己ベストは少数第一位までで保持されているので丸めてから比較する
				return (float)Math.Round(this.currentScore, 1) > GameManager.Instance.BestScore;
			}
		}

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// TODO: タップでスタート実装後はタップするまでカウント始まらないようにする
			this.IsProceedScoreCount = true;

			this.bestScoreText.text = this.ScoreToText(GameManager.Instance.BestScore);
		}

		/// <summary>
		/// 更新 (固定時間)
		/// </summary>
		private void FixedUpdate()
		{
			if (this.IsProceedScoreCount == true)
			{
				// スコア加算
				this.currentScore += Time.deltaTime * Constants.Game.ScorePerSecond;

				// スコア表示を更新
				currentScoreText.text = this.ScoreToText(this.currentScore);

				// 自己ベスト表示を更新
				this.UpdateBestScore();
			}
		}

		/// <summary>
		/// ゲームオーバー時処理
		/// </summary>
		public void GameOver()
		{
			this.IsProceedScoreCount = false;

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

			// リザルト画面で表示するために自己ベストの更新があったかを保持しておく
			bool isUpdateBestScore = this.isExceededBestScore;

			// 自己ベストを超えている場合は更新する
			// TODO: サーバーに送信する処理
			if (isUpdateBestScore == true)
			{
				GameManager.Instance.BestScore = this.currentScore;
			}

			// リザルト画面 (仮)
			// TODO: ちゃんとしたやつ
			DOVirtual.DelayedCall(1f, () =>
			{
				var button1 = GameObject.Instantiate(this.buttonPrefab);
				button1.SetLabel("タイトルへ戻る")
				.SetClickAction(() =>
				{
					SceneManager.Instance.Load<TitleScene>();
				});

				var button2 = GameObject.Instantiate(this.buttonPrefab);
				button2.SetLabel("リトライ")
				.SetClickAction(() =>
				{
					AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.pico22, 0.5f);
					SceneManager.Instance.Load<PlayGameScene>(this.parameter, LoadingManager.Types.FullscreenWithoutProgressbar);
				});

				var newRecord = isUpdateBestScore ? "<br><color=red>自己ベスト更新！</color>" : string.Empty;

				var popup = GameObject.Instantiate(this.poopupWindowPrefab, this.transform);
				popup.SetTitle("リザルト画面 (仮)")
				.AddButton(button1)
				.AddButton(button2)
				.SetCloseButtonActive(false)
				.SetMessage($"自己ベスト：{this.bestScoreText.text}<br>今回のスコア：{this.currentScoreText.text}{newRecord}")
				.Open();
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
		/// 自己ベストの表示を更新する
		/// </summary>
		/// <remarks>現在スコアが自己ベストを超えていなければ何もしない</remarks>
		private void UpdateBestScore()
		{
			// 自己ベストは少数第一位までで保持されているので丸めてから比較する
			if (this.isExceededBestScore == true)
			{
				this.bestScoreText.text = this.ScoreToText(this.currentScore);
			}
		}

		/// <summary>
		/// スコアを画面に表示する形式の文字列に変換
		/// </summary>
		private string ScoreToText(float score)
		{
			var roundScore = Math.Round(score, 1);
			return roundScore.ToString("F1") + " m";
		}
	}
}