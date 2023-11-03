using System;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// Login API レスポンス
	/// </summary>
	[Serializable]
	public class LoginResponse : ApiResponse
	{
		public string UserId;
		public string Name;
	}
}