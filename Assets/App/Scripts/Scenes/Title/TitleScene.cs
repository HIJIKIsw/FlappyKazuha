using Flappy.Api;
using Flappy.Common;
using Flappy.Manager;
using TMPro;
using UnityEngine;

namespace Flappy
{
	public class TitleScene : SceneBase
	{
		public override string Name => "Title";

		/// <summary>
		/// プレイヤー名
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI playerName;

		/// <summary>
		/// プレイヤー最高スコア
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI playerBestScore;

		/// <summary>
		/// プレイヤー最高原石獲得数
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI playerBestGemScore;

		/// <summary>
		/// プレイヤー累計スコア
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI playerAccumulatedScore;

		/// <summary>
		/// プレイヤー累計原石獲得数
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI playerAccumulatedGemScore;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// ログイン
			LoadingManager.Instance.Show(LoadingManager.Types.Overlay, 3);
			new LoginRequest().Request<LoginResponse>((loginResponse) =>
			{
				GameManager.Instance.UserId = loginResponse.UserId;
				this.playerName.text = loginResponse.Name;
				LoadingManager.Instance.CompleteTask();

				// 最高記録の取得
				new UserRankingRequest(loginResponse.UserId, UserRankingRequest.Division.Overall).Request<UserRankingResponse>((rankingResponse) =>
				{
					GameManager.Instance.BestScore = rankingResponse.Score;
					this.playerBestScore.text = GameManager.Instance.ScoreToText(GameManager.Instance.BestScore);
					this.playerBestGemScore.text = rankingResponse.GemScore.ToString();
					LoadingManager.Instance.CompleteTask();
				});

				// 統計情報の取得
				new UserStatsRequest(loginResponse.UserId).Request<UserStatsResponse>((statsResponse) =>
				{
					GameManager.Instance.AccumulatedScore = statsResponse.AccumulatedScore;
					GameManager.Instance.AccumulatedGemScore = statsResponse.AccumulatedGemScore;

					this.playerAccumulatedScore.text = GameManager.Instance.ScoreToText(statsResponse.AccumulatedScore);
					this.playerAccumulatedGemScore.text = statsResponse.AccumulatedGemScore.ToString();
					LoadingManager.Instance.CompleteTask();
				});
			});
		}

		public void TransitionHome()
		{
			SceneManager.Instance.Load<HomeScene>();
		}
	}
}