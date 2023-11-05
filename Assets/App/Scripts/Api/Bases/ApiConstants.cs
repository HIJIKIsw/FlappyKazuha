using System;
using System.Reflection;
using UnityEngine;

namespace Flappy.Api
{
	/// <summary>
	/// Apiで使用する定数
	/// </summary>
	public class ApiConstants
	{
		// ApiOverrideで上書きすることを前提にしている
		/// <summary>
		/// APIのURL
		/// </summary>
		public static string HttpRoot { private set; get; } = string.Empty;
		/// <summary>
		/// APIの認証キー
		/// </summary>
		public static string AuthKey { private set; get; } = string.Empty;
		/// <summary>
		/// APIのボディ暗号化キー
		/// </summary>
		public static string BodyEncryptKey { private set; get; } = string.Empty;

		/// <summary>
		/// オーバーライドするクラス名
		/// </summary>
		private const string OverrideClassFullName = "Flappy.Api.ApiConstantsOverride";

		/// <summary>
		/// ゲーム開始時にOverrideクラスの定義をチェックして、定義されていたらOverrideクラスで定義した値を保持しておく
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			Type overrideClassType = Type.GetType(ApiConstants.OverrideClassFullName);
			if (overrideClassType == null)
			{
				return;
			}

			// 各フィールドの値をオーバーライド
			ApiConstants.HttpRoot = ApiConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(HttpRoot));
			ApiConstants.AuthKey = ApiConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(AuthKey));
			ApiConstants.BodyEncryptKey = ApiConstants.GetStaticPropertyValueFromType(overrideClassType, nameof(BodyEncryptKey));
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