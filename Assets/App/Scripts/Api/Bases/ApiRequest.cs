using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Flappy.Utility;
using Unity.VisualScripting;
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
		public abstract string Url { get; }

		/// <summary>
		/// リクエストのパラメータ
		/// </summary>
		public Dictionary<string, string> Parameters { get; protected set; } = new Dictionary<string, string>();

		/// <summary>
		/// APIリクエスト実行
		/// </summary>
		/// <typeparam name="T">レスポンスの種類</typeparam>
		/// <param name="onSuccess">成功時のコールバック</param>
		/// <param name="onError">エラー発生時のコールバック</param>
		public virtual void Request<T>(UnityAction<T> onSuccess, UnityAction<Exception> onError = null) where T : ApiResponse, IDisposable, new()
		{
			// TODO: ApiManagerを実装する
			StaticCoroutine.Start(this.SendRequest<T>(onSuccess, onError));
		}

		protected IEnumerator SendRequest<T>(UnityAction<T> onSuccess, UnityAction<Exception> onError = null) where T : ApiResponse, IDisposable, new()
		{
			var bodyJson = new RequestBody(this.Parameters).ToJson();
			var encryptedParameter = EncryptionUtility.Encrypt(bodyJson, ApiConstants.BodyEncryptKey);
			var encryptedParameterBytes = Encoding.UTF8.GetBytes(encryptedParameter);
			var url = Path.Combine(ApiConstants.HttpRoot, this.Url);

			using (var post = new UnityWebRequest(url, "POST"))
			{
				post.uploadHandler = (UploadHandler)new UploadHandlerRaw(encryptedParameterBytes);
				post.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
				post.SetRequestHeader("Content-Type", "text/plain");
				yield return post.SendWebRequest();

				// リクエストの完了を待つ
				while (post.isDone == false)
				{
					yield return null;
				}

				switch (post.result)
				{
					case UnityWebRequest.Result.Success:
						// responseはFromJsonを実行するためだけのインスタンス
						// TODO: もっと良いやり方があったら書き直す
						using (T response = new T())
						{
							Debug.Log("APIリクエスト成功: " + post.downloadHandler.text);
							onSuccess?.Invoke(response.FromJson<T>(post.downloadHandler.text));
						}
						break;
					default:
						//TODO: エラーコードを返す
						//TODO: onError 設定していない時用の共通エラー処理
						Debug.Log("APIリクエストエラー: " + post.downloadHandler.text);
						onError?.Invoke(new Exception(post.downloadHandler.text));
						break;
				}
			}
		}

		/// <summary>
		/// リクエストのボディに使用する
		/// </summary>
		[Serializable]
		private class RequestBody
		{
			/// <summary>
			/// API認証キー
			/// </summary>
			public string ApiAuthKey = ApiConstants.AuthKey;
			/// <summary>
			/// リクエストパラメータ
			/// </summary>
			public SerializableDictionary<string, string> Parameter;

			public RequestBody(Dictionary<string, string> parameter)
			{
				this.Parameter = new SerializableDictionary<string, string>();
				this.Parameter.AddRange(parameter);
			}

			public string ToJson()
			{
				return JsonUtility.ToJson(this, false);
			}
		}

		/// <summary>
		/// Jsonシリアライズ可能なDictionary代替クラス
		/// </summary>
		/// <remarks>サーバサイドでは受け取ってから連想配列に変換している</remarks>
		[Serializable]
		public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
		{
			[SerializeField]
			private List<TKey> keys = new List<TKey>();
			[SerializeField]
			private List<TValue> values = new List<TValue>();

			public void OnBeforeSerialize()
			{
				keys.Clear();
				values.Clear();

				var e = GetEnumerator();

				while (e.MoveNext())
				{
					keys.Add(e.Current.Key);
					values.Add(e.Current.Value);
				}
			}

			public void OnAfterDeserialize()
			{
				this.Clear();

				int cnt = (keys.Count <= values.Count) ? keys.Count : values.Count;
				for (int i = 0; i < cnt; ++i)
					this[keys[i]] = values[i];

			}
		}
	}
}