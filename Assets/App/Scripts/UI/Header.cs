using TMPro;
using UnityEngine;
using Flappy.Manager;

namespace Flappy.UI
{
	public class Header : MonoBehaviour
	{
		[SerializeField]
		TextMeshProUGUI primogemValue;

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
			// TODO: パフォーマンス改善のため、値の変動があった時のみ更新する仕組みを考える
			primogemValue.text = GameManager.Instance.PrimogemCount.ToString();
		}
	}
}

