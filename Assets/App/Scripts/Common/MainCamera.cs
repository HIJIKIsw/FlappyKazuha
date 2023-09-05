using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Flappy.Manager;
using Flappy.Utility;

namespace Flappy.Common
{
	public class MainCamera : MonoBehaviour
	{
		bool isQuitting = false;

		void OnApplicationQuit()
		{
			this.isQuitting = true;
		}

		// TODO: もうちょいきれいに書きたい
		void OnEnable()
		{
			if (isQuitting == true)
			{
				return;
			}
			if (SceneManager.IsInitialized == false)
			{
				StaticCoroutine.Start(this.WaitForSceneManager(() =>
				{
					SceneManager.Instance.SetAlternativeCameraActive(false);
				}));
			}
			else
			{
				SceneManager.Instance.SetAlternativeCameraActive(false);
			}
		}

		// TODO: もうちょいきれいに書きたい
		void OnDisable()
		{
			if (isQuitting == true)
			{
				return;
			}
			if (SceneManager.IsInitialized == false)
			{
				StaticCoroutine.Start(this.WaitForSceneManager(() =>
				{
					SceneManager.Instance.SetAlternativeCameraActive(true);
				}));
			}
			else
			{
				SceneManager.Instance.SetAlternativeCameraActive(true);
			}
		}

		/// <summary>
		/// シーンマネージャのインスタンスが作られるまで待機
		/// </summary>
		IEnumerator WaitForSceneManager(UnityAction callback)
		{
			while (SceneManager.IsInitialized == false)
			{
				yield return null;
			}
			callback?.Invoke();
		}
	}
}