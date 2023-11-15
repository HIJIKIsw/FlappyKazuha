namespace Flappy.Api
{
	/// <summary>
	/// UserInfoUpdate API レスポンス
	/// </summary>
	public class UserInfoUpdateResponse : ApiResponse
	{
		/// <summary>
		/// ユーザID
		/// </summary>
		public int UserId;
		/// <summary>
		/// ユーザ名
		/// </summary>
		public string Name;
	}
}