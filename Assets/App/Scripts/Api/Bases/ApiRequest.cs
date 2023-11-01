using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Flappy.Api;
using Flappy.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Flappy.Api
{
	/// <summary>
	/// APIリクエストのベースクラス
	/// </summary>
	public abstract class ApiRequest : IApiRequest
	{
		/// <summary>
		/// APIのエンドポイント
		/// </summary>
		public string Url { get; }

		/// <summary>
		/// リクエストのパラメータ
		/// </summary>
		public Dictionary<string, string> Parameters { get; protected set; }

		/// <summary>
		/// APIリクエスト実行
		/// </summary>
		/// <typeparam name="T">レスポンスの種類</typeparam>
		/// <param name="onSuccess">成功時のコールバック</param>
		/// <param name="onError">エラー発生時のコールバック</param>
		public void Request<T>(UnityAction<T> onSuccess, UnityAction<Exception> onError = null) where T : ApiResponse
		{
			StaticCoroutine.Start(this.SendRequest<T>(onSuccess, onError));
		}

		private IEnumerator SendRequest<T>(UnityAction<T> onSuccess, UnityAction<Exception> onError = null) where T : ApiResponse
		{
			var parametersJson = JsonUtility.ToJson(this.Parameters, false);
			var encryptedParameter = EncryptionUtility.Encrypt(parametersJson, ApiConstants.BodyEncryptKey);
			var url = Path.Combine(ApiConstants.HttpRoot, this.Url);
			var post = UnityWebRequest.PostWwwForm(url, encryptedParameter);

			yield return post.SendWebRequest();

			switch (post.result)
			{
				case UnityWebRequest.Result.Success:
					// TODO: リクエストには成功したがサーバがエラーを返したケースの考慮
					Debug.Log("APIリクエスト成功");
					break;
				default:
					//TODO: エラー処理
					Debug.Log("APIリクエストエラー");
					break;
			}
		}
	}
}