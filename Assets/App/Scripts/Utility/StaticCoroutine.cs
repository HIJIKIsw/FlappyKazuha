using System.Collections;
using UnityEngine;

namespace Flappy.Utility
{
	public class StaticCoroutine : MonoBehaviour
	{
		public static void Start(IEnumerator coroutine)
		{
			var obj = new GameObject("StaticCoroutine");

			var component = obj.AddComponent<StaticCoroutine>();
			component.StartCoroutine(component.Do(coroutine));
		}

		IEnumerator Do(IEnumerator src)
		{
			yield return new WaitWhile(src.MoveNext);

			GameObject.Destroy(this.gameObject);
		}
	}
}