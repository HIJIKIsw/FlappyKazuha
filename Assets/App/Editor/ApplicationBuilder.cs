using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Flappy.Editor
{
	/// <summary>
	/// ビルド実行用エディタ拡張
	/// </summary>
	public class ApplicationBuilder
	{
		/// <summary>
		/// 開発用ビルドを実行
		/// </summary>
		[MenuItem("Build/WebGL/Development")]
		private static void BuildWebGLDevelopment()
		{
			ApplicationBuilder.BuildApplication(BuildTarget.WebGL, BuildOptions.Development, "Build/WebGL/Development");
		}

		/// <summary>
		/// 本番用ビルドを実行
		/// </summary>
		[MenuItem("Build/WebGL/Product")]
		private static void BuildWebGLProduct()
		{
			ApplicationBuilder.BuildApplication(BuildTarget.WebGL, BuildOptions.CleanBuildCache, "Build/WebGL/Product");
		}

		/// <summary>
		/// ビルドを実行
		/// </summary>
		/// <param name="buildTarget">ビルドターゲット</param>
		/// <param name="buildOptions">ビルドオプション</param>
		/// <param name="buildPath">ビルドパス</param>
		private static void BuildApplication(BuildTarget buildTarget, BuildOptions buildOptions, string buildPath)
		{
			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == false)
			{
				// 未保存のシーンがある場合、エラーメッセージを表示します
				Debug.LogError("ビルドを中止しました。未保存のシーンがあります。");
				return;
			}

			string[] scenePaths = GetScenePaths();

			BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
			buildPlayerOptions.scenes = scenePaths;
			buildPlayerOptions.locationPathName = buildPath;
			buildPlayerOptions.target = buildTarget;
			buildPlayerOptions.options = buildOptions;

			Debug.Log("Build started.");
			BuildPipeline.BuildPlayer(buildPlayerOptions);
			Debug.Log("Build finished.");
		}

		/// <summary>
		/// プロジェクト内のすべてのシーンファイルのパスを取得する
		/// </summary>
		private static string[] GetScenePaths()
		{
			EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
			string[] scenePaths = new string[scenes.Length];

			for (int i = 0; i < scenes.Length; i++)
			{
				scenePaths[i] = scenes[i].path;
			}

			return scenePaths;
		}
	}
}