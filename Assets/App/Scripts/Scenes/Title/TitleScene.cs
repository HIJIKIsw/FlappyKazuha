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

		[SerializeField]
		private TextMeshProUGUI playerName;

		[SerializeField]
		private TextMeshProUGUI playerBestScore;

		[SerializeField]
		private TextMeshProUGUI playerGems;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			LoadingManager.Instance.Show();
			new LoginRequest().Request<LoginResponse>((loginResponse) =>
			{
				new UserStatsRequest(loginResponse.UserId).Request<UserStatsResponse>((statsResponse) =>
				{
					GameManager.Instance.AccumulatedScore = statsResponse.AccumulatedScore;
					GameManager.Instance.AccumulatedGemScore = statsResponse.AccumulatedGemScore;

					this.playerName.text = loginResponse.Name;
					this.playerBestScore.text = GameManager.Instance.ScoreToText(statsResponse.AccumulatedScore);
					this.playerGems.text = statsResponse.AccumulatedGemScore.ToString();
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