namespace Flappy.Api
{
	/// <summary>
	/// UserRankingUpdate API リクエスト
	/// </summary>
	public class UserRankingUpdateRequest : ApiRequest
	{
		public override string Url => "user/ranking/update";
	}
}