using UnityEngine;

namespace Flappy.Manager
{
	//  ManagerSceneAutoLoader.cs
	//  http://kan-kikuchi.hatenablog.com/entry/ManagerSceneAutoLoader
	//
	//  Created by kan.kikuchi on 2016.08.04.

	/// <summary>
	/// Awake前にManagerSceneを自動でロードするクラス
	/// </summary>
	public static class ManagerSceneAutoLoader
	{
		const string MANAGER_SCENE_NAME = "ManagerContainer";

		// ゲーム開始時(シーン読み込み前)に実行される
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void LoadManagerScene()
		{
			// ManagerSceneが有効でない時(まだ読み込んでいない時)だけ追加ロードするように
			var managerScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(ManagerSceneAutoLoader.MANAGER_SCENE_NAME);
			if (managerScene.IsValid() == false)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(
					ManagerSceneAutoLoader.MANAGER_SCENE_NAME,
					UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}
		}
	}
}