using System;
using Flappy.Common;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// Login API リクエスト
	/// </summary>
	public class LoginRequest : ApiRequest
	{
		public override string Url => "login";

		public override bool IsLoginRequired => false;

		public LoginRequest()
		{
			// TODO: PlayerPrefsManager的なクラスを実装して置き換える
			string uuid;
			if (PlayerPrefs.HasKey(Constants.PlayerPrefsKeys.DeviceUuid) == true)
			{
				uuid = PlayerPrefs.GetString(Constants.PlayerPrefsKeys.DeviceUuid);
			}
			else
			{
				uuid = Guid.NewGuid().ToString();
				PlayerPrefs.SetString(Constants.PlayerPrefsKeys.DeviceUuid, uuid);
			}
			this.Parameters.Add("UUID", uuid);
		}
	}
}