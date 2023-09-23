using System.Runtime.InteropServices;
using UnityEngine.Events;
#if UNITY_EDITOR
using Flappy.Editor;
#endif

namespace Flappy.Utility
{
	public class JsLibUtility
	{
		/// <summary>
		/// flappy.jslib -> Prompt
		/// </summary>
		/// <returns></returns>
		[DllImport("__Internal")]
		private static extern string PromptInternal(string message, string defaultText);

		/// <summary>
		/// 文字入力欄を表示する
		/// </summary>
		/// <param name="message">表示するメッセージ</param>
		/// <param name="defaultText">入力欄の初期値</param>
		/// <param name="onClickOk">OKクリック時のアクション (入力値を受け取る)</param>
		/// <returns>入力がキャンセルされた、または何も入力されずOKを押下された場合はnullが返る</returns>
		public static void Prompt(string message, string defaultText = "", UnityAction<string> onClickOk = null)
		{
#if UNITY_EDITOR
			// UnityEditor 上ではエディタ拡張のPromptを使用する
			PromptOnEditor.ShowWindow(message, defaultText, onClickOk);
#else
			// キャンセルされた時にjslibから返る文字列
			const string canceledValue = "%canceled%";

			// キャンセルされた場合は null を返す
			var result = JsLibUtility.PromptInternal(message, defaultText);
			if( result == canceledValue )
			{
				result = null;
			}

			onClickOk?.Invoke(result);
#endif
		}
	}
}

