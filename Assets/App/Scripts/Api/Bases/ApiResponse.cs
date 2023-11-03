using System;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// APIレスポンスのベースクラス
	/// </summary>
	public abstract class ApiResponse : IApiResponse, IDisposable
	{
		/// <summary>
		/// Jsonからインスタンスを生成
		/// </summary>
		/// <remarks>
		/// virtualにして必要に応じてoverrideしたいのでstaticにはできない
		/// もし今後のバージョンアップでC# 11.0以上になったらできる
		/// </remarks>
		public virtual ApiResponse FromJson(string json)
		{
			return JsonUtility.FromJson<ApiResponse>(json);
		}

		/// <summary>
		/// インスタンスを破棄
		/// </summary>
		/// <remarks>必要に応じてオーバーライドして実装する</remarks>
		public virtual void Dispose()
		{
			// Nothind to do.
		}
	}
}