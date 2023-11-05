namespace Flappy.Api
{
	/// <summary>
	/// UserRankingUpdate API リクエスト
	/// </summary>
	public class UserRankingUpdateRequest : ApiRequest
	{
		public override string Url => "user/ranking/update";

		public UserRankingUpdateRequest(int userId, float score, int gemScore)
		{
			this.Parameters.Add("UserId", userId.ToString());
			this.Parameters.Add("Score", score.ToString());
			this.Parameters.Add("GemScore", gemScore.ToString());
		}
	}
}