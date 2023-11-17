using System;

namespace Flappy.Api
{
	/// <summary>
	/// UserInfo API レスポンス
	/// </summary>
	[Serializable]
	public class UserInfoResponse : ApiResponse
	{
		/// <summary>
		/// ユーザID
		/// </summary>
		public int UserId;

		/// <summary>
		/// ユーザ名
		/// </summary>
		public string UserName;
	}
}