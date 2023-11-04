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
			LoadingManager.Instance.Show();
			new LoginRequest().Request<LoginResponse>((loginResponse) =>
			{
				GameManager.Instance.UserId = loginResponse.UserId;
				new UserStatsRequest(loginResponse.UserId).Request<UserStatsResponse>((statsResponse) =>
				{
					GameManager.Instance.AccumulatedScore = statsResponse.AccumulatedScore;
					GameManager.Instance.AccumulatedGemScore = statsResponse.AccumulatedGemScore;

					this.playerName.text = loginResponse.Name;
					this.playerAccumulatedScore.text = GameManager.Instance.ScoreToText(statsResponse.AccumulatedScore);
					this.playerAccumulatedGemScore.text = statsResponse.AccumulatedGemScore.ToString();
					LoadingManager.Instance.CompleteTask();
				});
			});
		}

		/// <summary>
		/// プレイを開始
		/// </summary>
		public void StartPlayGame()
		{
			SceneManager.Instance.Load<PlayGameScene>();
		}
	}
}