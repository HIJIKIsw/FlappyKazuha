using UnityEditor;
using UnityEngine;

namespace Flappy.Editor
{
	public class EnvironmentSwitcher : EditorWindow
	{
#if ENV_LOCAL
		[MenuItem("Environment/✓Local")]
#else
		[MenuItem("Environment/　Local")]
#endif
		public static void SwitchLocalEnvironment()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, "ENV_LOCAL");
		}

#if ENV_DEVELOPMENT
		[MenuItem("Environment/✓Development")]
#else
		[MenuItem("Environment/　Development")]
#endif
		public static void SwitchDevelopmentEnvironment()
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, "ENV_DEVELOPMENT");
		}

#if ENV_TEST
		[MenuItem("Environment/✓Test")]
#else
		[MenuItem("Environment/　Test")]
#endif
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

