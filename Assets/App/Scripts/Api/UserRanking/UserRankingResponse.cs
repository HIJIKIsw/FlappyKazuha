using System;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// UserRanking API レスポンス
	/// </summary>
	[Serializable]
	public class UserRankingResponse : ApiResponse
	{
		public override bool UseCache => false;

		/// <summary>
		/// ランキング区分
		/// </summary>
		public UserRankingRequest.Division Division;

		/// <summary>
		/// 最高距離
		/// </summary>
		public float Score;

		/// <summary>
		/// 原石獲得数
		/// </summary>
		public int GemScore;
	}
}