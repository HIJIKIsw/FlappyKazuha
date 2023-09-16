namespace Flappy.Common
{
	/// <summary>
	/// 定数管理クラス
	/// </summary>
	public static class Constants
	{
		/// <summary>
		/// ゲーム挙動に関わる変数
		/// </summary>
		public static class Game
		{
			/// <summary>
			/// 1秒間に加算されるスコア
			/// </summary>
			public const float ScorePerSecond = 2f;
		}

		/// <summary>
		/// アセットアドレス
		/// </summary>
		public static class Assets
		{
			/// <summary>
			/// 画像アセット
			/// </summary>
			public static class Sprite
			{
				/// <summary>
				/// ボタンアイコン
				/// </summary>
				public enum ButtonIcon
				{
					Circle,
					Cross,
				}
			}

			/// <summary>
			/// 音アセット
			/// </summary>
			public static class Audio
			{
				/// <summary>
				/// Sound Effect
				/// </summary>
				public enum SE
				{
					pico22,
					kaifuku1,
					kaifuku2,
					boyon,
				}
			}
		}

	}
}