using System;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// UserRankingUpdate API レスポンス
	/// </summary>
	[Serializable]
	public class UserRankingUpdateResponse : ApiResponse
	{
		public override bool UseCache => false;

		// TODO: 更新されたランキング種類や順位の受け取り
	}
}