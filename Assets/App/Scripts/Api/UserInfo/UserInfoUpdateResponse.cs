using System;

namespace Flappy.Api
{
	/// <summary>
	/// UserInfoUpdate API レスポンス
	/// </summary>
	[Serializable]
	public class UserInfoUpdateResponse : UserInfoResponse
	{
		public override Type CacheType => typeof(UserInfoResponse);
	}
}