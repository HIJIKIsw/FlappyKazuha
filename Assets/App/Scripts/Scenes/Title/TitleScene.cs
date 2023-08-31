using System.Collections;
using System.Collections.Generic;
using Flappy.Common;
using Flappy.Manager;
using UnityEngine;

namespace Flappy
{
	public class TitleScene : SceneBase
	{
		public override string Name => "Title";

		public void StartPlayGame()
		{
			SceneManager.Instance.Load<PlayGameScene>();
		}
	}
}