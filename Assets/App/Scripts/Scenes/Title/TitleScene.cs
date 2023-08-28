using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
	public class TitleScene : MonoBehaviour, ISceneBase
	{
		public static string Name
		{
			get
			{
				return nameof(TitleScene);
			}
		}
	}
}