using System;
using Flappy.Manager;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// UserInfoUpdate API レスポンス
	/// </summary>
	[Serializable]
	public class UserInfoUpdateResponse : UserInfoResponse
	{
		public override Type CacheType => typeof(UserInfoResponse);

		public override T FromJson<T>(string json)
		{
			return this.FromJson(json) as T;
		}

		public UserInfoUpdateResponse FromJson(string json)
		{
			var response = JsonUtility.FromJson<UserInfoUpdateResponse>(json);

			// GameManager側の値を更新しておく
			// MEMO: UserIdが変更されることはないので更新は不要
			if (string.IsNullOrEmpty(response.UserName) == false)
			{
				GameManager.Instance.UserName = response.UserName;
			}

			return response;
		}
	}
}