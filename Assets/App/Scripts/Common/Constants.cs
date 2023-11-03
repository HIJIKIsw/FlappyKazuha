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
		/// PlayerPrefsに使用するキー
		/// </summary>
		/// <remarks>summaryコメントに型名(string/int/floatを書くこと)</remarks>
		/// TODO: PlayerPrefsManager的なクラスを実装したらそちらに移動する
		public static class PlayerPrefsKeys
		{
			/// <summary>
			/// デバイスUUID: string
			/// </summary>
			public const string DeviceUuid = "DeviceUuid";
		}

		/// <summary>
		/// アセットアドレス
		/// </summary>
		public static class Assets
		{
			// MEMO: ここにenumを追加した場合、AssetAddressUtility.directoryMapにも対応する定義を追加すること

			/// <summary>
			/// プレハブ
			/// </summary>
			public static class Prefab
			{
				/// <summary>
				/// プレイヤー
				/// </summary>
				public enum Player
				{
					Kazuha,
				}
			}

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