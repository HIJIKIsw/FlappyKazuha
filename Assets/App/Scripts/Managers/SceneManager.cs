using System;
using System.Collections.Generic;
using System.Linq;
using Flappy.Common;
using UnityEngine;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Flappy.Manager
{
	public class SceneManager : SingletonMonoBehaviour<SceneManager>
	{
		/// <summary>
		/// コンストラクタを隠蔽する
		/// </summary>
		SceneManager()
		{
			// Nothing to do.
		}

		/// <summary>
		/// シーンコンポーネントを一時的にアタッチするGameObject
		/// </summary>
		/// <remarks>
		/// クラス名からシーン名を取得する際、シーンクラスを一度インスタンス化する必要があるが、
		/// AwakeやStartが走らないように、常に非アクティブなGameObjectを一時アタッチ場所として使う。
		/// このオブジェクトは常に非アクティブを担保すること。
		/// </remarks>
		[SerializeField]
		GameObject sceneComponentContainer;

		/// <summary>
		/// 管理対象のシーン
		/// </summary>
		HashSet<string> managedSceneNames = new HashSet<string>();

		void Start()
		{
			// 初期シーン自動取得
			var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			var rootGameObjects = scene.GetRootGameObjects();
			foreach (var rootGameObject in rootGameObjects)
			{
				var sceneComponent = rootGameObject.GetComponent<SceneBase>();
				if (sceneComponent != null)
				{
					this.managedSceneNames.Add(sceneComponent.Name);
					break;
				}
			}

			if (this.managedSceneNames.Any() == true)
			{
				Debug.Log("Scene loaded: " + this.managedSceneNames.First());
			}
			else
			{
				Debug.LogAssertion("Could not get a valid scene.");
			}
		}

		/// <summary>
		/// シーンを読み込む
		/// </summary>
		/// <typeparam name="T">読み込むシーンクラス</typeparam>
		/// <param name="parameter">シーンに渡すパラメータ</param>
		/// <param name="isAdditive">シーンを追加で読み込むか</param>
		public void Load<T>(SceneParameter parameter = null, bool isAdditive = false) where T : SceneBase
		{
			// シーン名を取得
			var sceneName = this.GetSceneName<T>();

			// 同名シーンは複数読み込み不可
			if (this.managedSceneNames.Contains(sceneName) == true)
			{
				Debug.LogAssertion($"The specified scene \"{sceneName}\" is already exists.");
				return;
			}

			if (this.ExistsSceneFile(sceneName) == false)
			{
				Debug.LogAssertion($"Could not find scene \"{sceneName}\".");
				return;
			}

			var asyncOperation = USceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
			this.managedSceneNames.Add(sceneName);

			// TODO: メソッド抽出などしてきれいに書き直す
			asyncOperation.completed += (op) =>
			{
				var scene = USceneManager.GetSceneByName(sceneName);
				var rootGameObjects = scene.GetRootGameObjects();
				SceneBase sceneComponent = null;
				foreach (var rootGameObject in rootGameObjects)
				{
					sceneComponent = rootGameObject.GetComponent<SceneBase>();
					if (sceneComponent != null)
					{
						break;
					}
				}

				if (sceneComponent != null)
				{
					sceneComponent.Initialize(parameter);
				}
				else
				{
					Debug.LogAssertion("Failed to get the loaded scene.");
				}
			};

			// TODO: Loading2呼び出し周りの処理実装
			// TODO: シーンを置き換える場合の処理実装する
		}

		/// <summary>
		/// シーンをアンロード
		/// </summary>
		/// <typeparam name="T">アンロードするシーンクラス</typeparam>
		public void Unload<T>() where T : SceneBase
		{
			var sceneName = this.GetSceneName<T>();
			var scene = USceneManager.GetSceneByName(sceneName);
			if (scene.IsValid() == false)
			{
				Debug.LogAssertion($"The specified scene \"{sceneName}\" has not been loaded.");
				return;
			}
			USceneManager.UnloadSceneAsync(sceneName);
			this.managedSceneNames.Remove(sceneName);
		}

		private string GetSceneName<T>() where T : SceneBase
		{
			// TODO: もっと良いやり方があったら直す
			this.sceneComponentContainer.SetActive(false);
			var sceneComponent = (SceneBase)this.sceneComponentContainer.AddComponent(typeof(T));
			var sceneName = sceneComponent.Name;
			GameObject.Destroy(sceneComponent);
			return sceneName;
		}

		private bool ExistsSceneFile(string name)
		{
			for (int i = 0; i < USceneManager.sceneCountInBuildSettings; i++)
			{
				string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
				string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
				if (sceneNameFromPath == name)
				{
					return true;
				}
			}

			return false;
		}
	}
}