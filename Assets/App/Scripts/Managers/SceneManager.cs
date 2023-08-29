using System;
using System.Collections.Generic;
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
		/// 現在のシーン
		/// </summary>
		SceneBase currentScene = null;
		List<SceneBase> additiveScenes = new List<SceneBase>();

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
					this.currentScene = sceneComponent;
					break;
				}
			}

			if (this.currentScene != null)
			{
				Debug.Log("Scene loaded: " + this.currentScene.Name);
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
		/// <param name="isAdditive">シーンを追加で読み込むか</param>
		public void Load<T>(bool isAdditive = false) where T : SceneBase
		{
			// シーン名を取得
			sceneComponentContainer.SetActive(false);
			var sceneComponent = sceneComponentContainer.AddComponent<T>();
			var sceneName = sceneComponent.Name;
			GameObject.Destroy(sceneComponent);

			if (this.ExistsScene(sceneName) == false)
			{
				Debug.LogAssertion($"Could not find scene \"{sceneName}\".");
				return;
			}

			USceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
			USceneManager.UnloadSceneAsync(currentScene.name);

			// TODO: シーンにパラメータを渡す処理周りの実装
			// TODO: Loading2呼び出し周りの処理実装
			// TODO: シーンを追加で読み込む場合の処理実装する
			// TODO: currentScene を更新する
		}

		private bool ExistsScene(string name)
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