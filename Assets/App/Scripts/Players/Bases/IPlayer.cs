namespace Flappy
{
	/// <summary>
	/// Player インターフェイス
	/// </summary>
	public interface IPlayer
	{
		/// <summary>
		/// 死亡フラグ
		/// </summary>
		bool IsDead { get; }
	}
}