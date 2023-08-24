using UnityEngine;
using UnityEngine.UI;

namespace Flappy.UI
{
	/// <summary>
	/// フルスクリーンロード画面
	/// </summary>
	public class Loading2 : MonoBehaviour
	{
		[SerializeField]
		Image progressBarFill;

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		public void SetProgress(float progress)
		{
			progress = Mathf.Clamp(progress, 0f, 1f);
			this.progressBarFill.fillAmount = progress;
		}
	}
}