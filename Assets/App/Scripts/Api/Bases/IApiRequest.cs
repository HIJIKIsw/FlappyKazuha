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
		/// ログイン情報が必要なAPI領域か
		/// </summary>
		bool IsLoginRequired { get; }

		/// <summary>
		/// リクエストのパラメータ
		/// </summary>
		Dictionary<string, string> Parameters { get; }
	}
}