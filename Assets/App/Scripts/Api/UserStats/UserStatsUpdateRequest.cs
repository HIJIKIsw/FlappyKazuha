using Flappy.Manager;

namespace Flappy.Api
{
	/// <summary>
	/// UserStatsUpdate API リクエスト
	/// </summary>
	public class UserStatsUpdateRequest : ApiRequest
	{
		public override string Url => "user/stats/update";

		public UserStatsUpdateRequest(float accumulatedScore, int accumulatedGemScore)
		{
			this.Parameters.Add("UserId", GameManager.Instance.UserId.ToString());
			this.Parameters.Add("AccumulatedScore", accumulatedScore.ToString());
			this.Parameters.Add("AccumulatedGemScore", accumulatedGemScore.ToString());
		}
	}
}