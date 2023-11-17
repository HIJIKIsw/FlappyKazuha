namespace Flappy.Api
{
	/// <summary>
	/// UserInfoUpdate API リクエスト
	/// </summary>
	public class UserInfoUpdateRequest : ApiRequest
	{
		public override string Url => "user/info/update";

		public UserInfoUpdateRequest(string name)
		{
			this.Parameters.Add("Name", name);
		}
	}
}