using System;

namespace Flappy.Api
{
	/// <summary>
	/// UserStatsUpdate API レスポンス
	/// </summary>
	[Serializable]
	public class UserStatsUpdateResponse : UserStatsResponse
	{
		public override Type CacheType => typeof(UserStatsResponse);
	}
}