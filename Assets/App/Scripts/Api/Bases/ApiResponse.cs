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
		/// キャッシュを使用するレスポンスタイプか
		/// </summary>
		public virtual bool UseCache { get; } = true;

		/// <summary>
		/// キャッシュ時のタイプ
		/// </summary>
		/// <remarks></remarks>
		public virtual Type CacheType { get; } = null;

		/// <summary>
		/// Jsonからインスタンスを生成
		/// </summary>
		/// <remarks>
		/// virtualにして必要に応じてoverrideしたいのでstaticにはできない
		/// もし今後のバージョンアップでC# 11.0以上になったらできる
		/// </remarks>
		public virtual T FromJson<T>(string json) where T : ApiResponse
		{
			return JsonUtility.FromJson<T>(json);
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