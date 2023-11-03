#if ENV_LOCAL || ENV_DEVELOPMENT
using System;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// HelloWorld API レスポンス
	/// </summary>
	[Serializable]
	public class HelloWorldResponse : ApiResponse
	{
		public string Message;

		/// <summary>
		/// FromJsonをオーバーライドする場合はこう書く
		/// </summary>
		public override T FromJson<T>(string json)
		{
			// ここでResponseタイプに応じた処理
			return JsonUtility.FromJson<T>(json);
		}
	}
}
#endif