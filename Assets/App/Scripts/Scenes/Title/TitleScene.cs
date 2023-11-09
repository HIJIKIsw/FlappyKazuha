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
		/// CharacterListオブジェクト
		/// </summary>
		[SerializeField]
		private TMP_Dropdown characterList;

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
				new UserRankingRequest(UserRankingRequest.Division.Overall).Request<UserRankingResponse>((rankingResponse) =>
				{
					GameManager.Instance.BestScore = rankingResponse.Score;
					this.playerBestScore.text = GameManager.Instance.ScoreToText(GameManager.Instance.BestScore);
					this.playerBestGemScore.text = rankingResponse.GemScore.ToString();
					LoadingManager.Instance.CompleteTask();
				});

				// 統計情報の取得
				new UserStatsRequest().Request<UserStatsResponse>((statsResponse) =>
				{
					GameManager.Instance.AccumulatedScore = statsResponse.AccumulatedScore;
					GameManager.Instance.AccumulatedGemScore = statsResponse.AccumulatedGemScore;

					this.playerAccumulatedScore.text = GameManager.Instance.ScoreToText(statsResponse.AccumulatedScore);
					this.playerAccumulatedGemScore.text = statsResponse.AccumulatedGemScore.ToString();
					LoadingManager.Instance.CompleteTask();
				});
			});
		}

		/// <summary>
		/// Titleシーンで「Start」ボタンを押した時に実行される
		/// </summary>
		public void StartPlayGame()
		{
			// シーンに渡すパラメータを生成
			SceneParameter parameter = new SceneParameter();

			// 選択されたキャラクターに応じてパラメータに値をセット
			switch (characterList.value)
			{
				case 0:
					{
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
						break;
					}
				case 1:
					{
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Wanderer);
						break;
					}
				default:
					{
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
						break;
					}
			}

			// PlayGameSceneにパラメータを渡す
			SceneManager.Instance.Load<PlayGameScene>(parameter);
		}
	}
}