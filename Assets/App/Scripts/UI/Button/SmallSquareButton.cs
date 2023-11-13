using TMPro;

namespace Flappy.UI
{
	/// <summary>
	/// 小さい四角ボタン
	/// </summary>
	public class SmallSquareButton : CommonButton
	{
		/// <summary>
		/// SmallSquareButtonはLabelを使用できない
		/// </summary>
		public new SmallSquareButton SetLabel(string label)
		{
			return this;
		}
	}
}