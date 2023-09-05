using System;
using System.Collections.Generic;
using System.Linq;
using Flappy.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		/// シーン遷移中にシーン内カメラの代わりに使用するカメラ
		/// </summary>
		[SerializeField]
		Camera alternativeCamera;

		/// <summary>
		/// 現在のシーン
		/// </summary>
		public SceneBase CurrentScene { get; private set; }

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
					this.CurrentScene = sceneComponent;
					break;
				}
			}

			if (this.CurrentScene != null)
			{
				Debug.Log("Scene loaded: " + this.CurrentScene.Name);
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
		public void Load<T>(SceneParameter parameter = null) where T : SceneBase
		{
			// シーン名を取得
			var sceneName = this.GetSceneName<T>();

			// 同名シーンは読み込み不可
			if (sceneName == this.CurrentScene.Name)
			{
				Debug.LogAssertion($"The specified scene \"{sceneName}\" is already loaded.");
				return;
			}

			if (this.ExistsSceneFile(sceneName) == false)
			{
				Debug.LogAssertion($"Could not find scene \"{sceneName}\".");
				return;
			}

			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku1, 0.1f, 0.8f);
			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku2, 0.4f, 2.5f);

			// TODO: メソッド抽出などしてきれいに書き直す
			LoadingManager.Instance.ShowFullscreen(2, () =>
			{
				var unloadAsynOperation = USceneManager.UnloadSceneAsync(this.CurrentScene.Name);
				var loadAsyncOperation = USceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

				this.CurrentScene = null;

				// TODO: メソッド抽出などしてきれいに書き直す
				unloadAsynOperation.completed += (op) =>
				{
					LoadingManager.Instance.CompleteTask();
				};
				loadAsyncOperation.completed += (op) =>
				{
					var scene = USceneManager.GetSceneByName(sceneName);
					var rootGameObjects = scene.GetRootGameObjects();
					foreach (var rootGameObject in rootGameObjects)
					{
						this.CurrentScene = rootGameObject.GetComponent<SceneBase>();
						if (this.CurrentScene != null)
						{
							break;
						}
					}

					if (this.CurrentScene != null)
					{
						this.CurrentScene.SetActive(false);
						this.CurrentScene.Initialize(parameter);
					}
					else
					{
						Debug.LogAssertion("Failed to get the loaded scene.");
					}

					LoadingManager.Instance.CompleteTask();
				};
			}, () =>
			{
				if (this.CurrentScene != null)
				{
					this.CurrentScene.SetActive(true);
					USceneManager.SetActiveScene(this.CurrentScene.Scene);
				}
			});
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

		public void SetAlternativeCameraActive(bool value)
		{
			this.alternativeCamera.gameObject.SetActive(value);
		}
	}
}