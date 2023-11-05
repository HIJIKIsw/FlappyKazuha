namespace Flappy.Api
{
	/// <summary>
	/// UserStats API リクエスト
	/// </summary>
	public class UserStatsRequest : ApiRequest
	{
		public override string Url => "user/stats";

		public UserStatsRequest(int userId)
		{
			this.Parameters.Add("UserId", userId.ToString());
		}
	}
}