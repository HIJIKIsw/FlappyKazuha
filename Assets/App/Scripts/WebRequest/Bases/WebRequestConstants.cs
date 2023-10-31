using System;
using System.Reflection;
using UnityEngine;

namespace Flappy.WebRequest
{
	/// <summary>
	/// WebRequestで使用する定数
	/// </summary>
	public class WebRequestConstants
	{
		// WebRequestConstantsOverrideで上書きすることを前提にしている
		/// <summary>
		/// APIのURL
		/// </summary>
		public static string ApiHttpRoot { private set; get; } = string.Empty;
		/// <summary>
		/// APIの認証キー
		/// </summary>
		public static string ApiAuthKey { private set; get; } = string.Empty;
		/// <summary>
		/// APIのボディ暗号化キー
		/// </summary>
		public static string ApiBodyEncryptKey { private set; get; } = string.Empty;

		/// <summary>
		/// オーバーライドするクラス名
		/// </summary>
		private const string OverrideClassFullName = "Flappy.WebRequest.WebRequestConstantsOverride";

		/// <summary>
		/// ゲーム開始時にOverrideクラスの定義をチェックして、定義されていたらOverrideクラスで定義した値を保持しておく
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			Type overrideClassType = Type.GetType(WebRequestConstants.OverrideClassFullName);
			if (overrideClassType == null)
			{
				return;
			}

			// 各フィールドの値をオーバーライド
			WebRequestConstants.ApiHttpRoot = WebRequestConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(ApiHttpRoot));
			WebRequestConstants.ApiAuthKey = WebRequestConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(ApiAuthKey));
			WebRequestConstants.ApiBodyEncryptKey = WebRequestConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(ApiBodyEncryptKey));
		}

		/// <summary>
		/// クラスのstaticプロパティから値を取得
		/// </summary>
		private static string GetStaticPropertyValueFromType(Type classType, string fieldName)
		{
			var property = classType.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Static);
			if (property == null || property.PropertyType != typeof(string))
			{
				return string.Empty;
			}
			return property.GetValue(null).ToString();
		}
	}
}