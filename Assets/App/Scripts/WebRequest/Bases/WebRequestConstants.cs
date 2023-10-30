using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.WebRequest
{
	/// <summary>
	/// WebRequstで使う定数
	/// </summary>
	public class WebRequestConstants
	{
#if ENV_DEVELOPMENT
		/// <summary>
		/// APIのURL (開発)
		/// </summary>
		public const string ApiHttpRoot = "http://hogehoge.dev/";
		/// <summary>
		/// APIの認証キー
		/// </summary>
		public const string ApiAuthKey = "hogehogehoge";
		/// <summary>
		/// APIのボディ暗号化キー
		/// </summary>
		public const string ApiBodyEncryptKey = "fugafugafuga";
#elif ENV_TEST
		/// <summary>
		/// APIのURL (テスト)
		/// </summary>
		public const string ApiHttpRoot = "http://hogehoge.stg/";
		/// <summary>
		/// APIの認証キー
		/// </summary>
		public const string ApiAuthKey = "hogehogehoge";
		/// <summary>
		/// APIのボディ暗号化キー
		/// </summary>
		public const string ApiBodyEncryptKey = "fugafugafuga";
#else
		/// <summary>
		/// APIのURL (本番)
		/// </summary>
		public const string ApiHttpRoot = "http://hogehoge.prd/";
		/// <summary>
		/// APIの認証キー
		/// </summary>
		public const string ApiAuthKey = "hogehogehoge";
		/// <summary>
		/// APIのボディ暗号化キー
		/// </summary>
		public const string ApiBodyEncryptKey = "fugafugafuga";
#endif
	}
}