using UnityEditor;
using UnityEngine;

namespace Flappy.Editor
{
	public class EnvironmentSwitcher : EditorWindow
	{
		[MenuItem("Environment/Development")]
		public static void SwitchDevelopmentEnvironment()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, "ENV_DEVELOPMENT");
		}

		[MenuItem("Environment/Test")]
		public static void SwitchTestEnvironment()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, "ENV_TEST");
		}

		// [MenuItem("Environment/Production")]
		public static void SwitchProductionEnvironment()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, "");
		}
	}
}

