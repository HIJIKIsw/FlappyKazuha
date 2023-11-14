using UnityEngine;

namespace Flappy.UI
{
	/// <summary>
	/// 小さい四角ボタン
	/// </summary>
	public class SmallSquareButton : CommonButton
	{
		/// <summary>
		/// 使用できないメソッドのため呼ばれたらエラー出力して処理続行
		/// </summary>
		/// <remarks>メソッドチェーン可</remarks>
		public new SmallSquareButton SetLabel(string label)
		{
			Debug.LogAssertion("This method is not supported.");
			return this;
		}
	}
}