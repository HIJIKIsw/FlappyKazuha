namespace Flappy.Api
{
	/// <summary>
	/// UserRanking API リクエスト
	/// </summary>
	public class UserRankingRequest : ApiRequest
	{
		public override string Url => "user/ranking";

		/// <summary>
		/// ランキング区分
		/// </summary>
		/// <remarks>定義はサーバ側と同期する必要があるため変更の際は注意</remarks>
		public enum Division
		{
			Overall,
			Weekly,
			Monthly,
		}

		/// <summary>
		/// ランキング種類
		/// </summary>
		/// <remarks>定義はサーバ側と同期する必要があるため変更の際は注意</remarks>
		public enum Type
		{
			Score,
			GemScore,
		}

		public UserRankingRequest(int userId, UserRankingRequest.Division division)
		{
			this.Parameters.Add("UserId", userId.ToString());
			this.Parameters.Add("Division", division.ToString());
		}
	}
}