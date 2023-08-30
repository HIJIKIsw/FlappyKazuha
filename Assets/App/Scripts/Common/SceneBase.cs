using UnityEngine;

namespace Flappy.Common
{
	public abstract class SceneBase : MonoBehaviour, IScene
	{
		public abstract string Name { get; }
	}
}