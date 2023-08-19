using Flappy.Common;

namespace Flappy.Manager
{
	/// <summary>
	///  ゲームマネージャ
	/// </summary>
	/// <remarks>シングルトンクラス</remarks>
	public class GameManager : SingletonMonoBehaviour<GameManager>
	{
		/// <summary>
		/// コンストラクタを隠蔽する
		/// </summary>
		GameManager()
		{
			// Nothing to do.
		}

		/// <summary>
		/// 原石の所持数
		/// </summary>
		/// TODO: 所持品クラスみたいなものを作ってそっちにまとめる (原石、ネームプレート、称号など)
		public int PrimogemCount { get; set; } = 0;
	}
}

