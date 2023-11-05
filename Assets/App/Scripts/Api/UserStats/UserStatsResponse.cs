using System;

namespace Flappy.Api
{
	/// <summary>
	/// UserStats API レスポンス
	/// </summary>
	[Serializable]
	public class UserStatsResponse : ApiResponse
	{
		public float AccumulatedScore;
		public int AccumulatedGemScore;
	}
}