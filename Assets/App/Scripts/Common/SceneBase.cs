using UnityEngine;

namespace Flappy
{
	public abstract class SceneBase : MonoBehaviour, IScene
	{
		public abstract string Name { get; }
	}
}