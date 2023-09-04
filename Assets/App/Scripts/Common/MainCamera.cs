using System.Collections;
using System.Collections.Generic;
using Flappy.Manager;
using UnityEngine;

namespace Flappy.Common
{
	public class MainCamera : MonoBehaviour
	{
		void OnEnable()
		{
			SceneManager.Instance.SetAlternativeCameraActive(false);
		}

		void OnDisable()
		{
			SceneManager.Instance.SetAlternativeCameraActive(true);
		}
	}
}