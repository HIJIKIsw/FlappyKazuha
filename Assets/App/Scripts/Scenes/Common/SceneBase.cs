using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
	public abstract class SceneBase : MonoBehaviour, ISceneBase
	{
		public abstract string Name { get; }
	}
}