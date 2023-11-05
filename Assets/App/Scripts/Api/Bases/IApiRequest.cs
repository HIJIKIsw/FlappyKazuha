using System.Collections.Generic;

namespace Flappy.Api
{
	/// <summary>
	/// APIリクエストインターフェイス
	/// </summary>
	public interface IApiRequest
	{
		/// <summary>
		/// APIのエンドポイント
		/// </summary>
		string Url { get; }

		/// <summary>
		/// リクエストのパラメータ
		/// </summary>
		Dictionary<string, string> Parameters { get; }
	}
}