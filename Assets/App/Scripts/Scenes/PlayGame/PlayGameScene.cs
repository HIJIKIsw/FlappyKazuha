using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using DG.Tweening;
using Flappy.Common;
using Flappy.Gimmicks;
using Flappy.Manager;
using Flappy.UI;
using Flappy.Utility;
using Flappy.PlayGame;
using Flappy.Api;

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
		/// PlayGameSceneシーンが取りうるパラメータの種類
		/// </summary>
		public enum Parameters
		{
			Character,
		}

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
		/// HeightLimitオブジェクト
		/// </summary>
		[SerializeField]
		private HeightLimit heightLimit;

		/// <summary>
		/// ScoreGaugeオブジェクト
		/// </summary>
		[SerializeField]
		private ScoreGauge scoreGauge;

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
		/// Playerオブジェクト
		/// </summary>
		private PlayerBase player;

		/// <summary>
		/// スコア加算フラグ
		/// </summary>
		public bool IsProceedScoreCount { get; private set; } = false;

		/// <summary>
		/// ゲームオーバーフラグ
		/// </summary>
		public bool IsGameOver { get; private set; } = false;

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
				return GameManager.Instance.RoundScore(this.currentScore) > GameManager.Instance.BestScore;
			}
		}

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// 原石獲得数をリセット
			// TODO: この変数はPlayGameScene側で持ちたい
			GameManager.Instance.PrimogemCount = 0;

			// TODO: タップでスタート実装後はタップするまでカウント始まらないようにする
			this.IsProceedScoreCount = true;

			// TODO: Playerのインスタンス生成はInitializeで処理するようにする (そうなったらシーンの無効→有効は要らない)

			this.SetActive(false);

			// パラメータから値を取り出す
			var character = this.Parameter[PlayGameScene.Parameters.Character] as Enum;
			var address = AssetAddressUtility.GetAssetAddress(character);
			var handle = Addressables.LoadAssetAsync<GameObject>(address);
			handle.Completed += (op) =>
			{
				// Playerインスタンスを生成
				this.player = GameObject.Instantiate(op.Result, this.transform).GetComponent<PlayerBase>();

				// HeightLimitにPlayerインスタンスへの参照を通知
				this.heightLimit.SetPlayerInstance(this.player);

				this.SetActive(true);
			};
		}

		/// <summary>
		/// 更新 (1フレーム)
		/// </summary>
		private void Update()
		{
			// プレイヤーが死んでいたらゲームオーバーにする
			if (this.IsGameOver == false && this.player.IsDead == true)
			{
				this.IsGameOver = true;
				this.GameOver();
			}
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

				// スコアゲージを更新
				this.scoreGauge.SetCurrentScore(this.currentScore);
			}
		}

		/// <summary>
		/// ゲームオーバー時処理
		/// </summary>
		public void GameOver()
		{
			this.IsProceedScoreCount = false;

			// ユーザランキング情報を更新
			this.UpdateUserRanking();

			// ユーザ統計情報を更新
			this.UpdateUserStats();

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

			// 超える前の自己ベストを保持しておく (リザルト用)
			var oldBestScore = GameManager.Instance.BestScore;

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
					SceneManager.Instance.Load<PlayGameScene>(this.Parameter, LoadingManager.Types.FullscreenWithoutProgressbar);
				});

				var newRecord = isUpdateBestScore ? "<br><color=red>自己ベスト更新！</color>" : string.Empty;

				var popup = GameObject.Instantiate(this.poopupWindowPrefab, this.transform);
				popup.SetTitle("リザルト画面 (仮)")
				.AddButton(button1)
				.AddButton(button2)
				.SetCloseButtonActive(false)
				.SetMessage($"自己ベスト：{GameManager.Instance.ScoreToText(oldBestScore)}<br>今回のスコア：{GameManager.Instance.ScoreToText(this.currentScore)}{newRecord}")
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
		/// ユーザ統計情報を更新
		/// </summary>
		/// TODO: 汚いので書き直す。そもそもここでやるべきかどうか。
		private void UpdateUserStats()
		{
			GameManager.Instance.AccumulatedScore += this.currentScore;
			GameManager.Instance.AccumulatedGemScore += GameManager.Instance.PrimogemCount;

			// TODO: この時点ではLoadingを表示しないようにして、リザルト画面表示前にリクストが終わってなかったらLoadingを表示するようにする
			LoadingManager.Instance.Show();
			new UserStatsUpdateRequest(
				GameManager.Instance.AccumulatedScore,
				GameManager.Instance.AccumulatedGemScore
			).Request<UserStatsUpdateResponse>((response) =>
			{
				LoadingManager.Instance.CompleteTask();
			});
		}

		/// <summary>
		/// ユーザランキング情報を更新
		/// </summary>
		private void UpdateUserRanking()
		{
			// TODO: この時点ではLoadingを表示しないようにして、リザルト画面表示前にリクストが終わってなかったらLoadingを表示するようにする
			// TODO: ユーザIDはどこかにキャッシュしておくようにする
			LoadingManager.Instance.Show();
			new LoginRequest().Request<LoginResponse>((loginResponse) =>
			{
				new UserRankingUpdateRequest(
					GameManager.Instance.RoundScore(this.currentScore),
					GameManager.Instance.PrimogemCount
				).Request<UserRankingUpdateResponse>((response) =>
				{
					LoadingManager.Instance.CompleteTask();
				});
			});

		}
	}
}