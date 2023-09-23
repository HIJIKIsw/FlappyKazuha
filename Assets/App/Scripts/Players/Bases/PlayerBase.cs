using Flappy.Manager;
using UnityEngine;

namespace Flappy
{
	/// <summary>
	/// Player ベースクラス
	/// </summary>
	public class PlayerBase : MonoBehaviour, IPlayer
	{
		/// <summary>
		/// Rigidbody2Dコンポーネント
		/// </summary>
		[SerializeField]
		protected Rigidbody2D rb2D;

		/// <summary>
		/// 死亡フラグ
		/// </summary>
		public bool IsDead => this.isDead;
		private bool isDead = false;

		/// <summary>
		/// PlayGameSceneオブジェクト
		/// </summary>
		/// TODO: こっちでシーンへの参照を持つのではなく、シーン側がPlayer側を監視したりするようにする
		[SerializeField]
		PlayGameScene playGameScene;

		/// <summary>
		/// 速度をセット
		/// </summary>
		/// <param name="speed">速度 (Rigidbody2Dのvelocityに渡す値)</param>
		protected void SetSpeed(Vector2 speed)
		{
			this.rb2D.velocity = speed;
		}

		/// <summary>
		/// 指定した座標に移動
		/// </summary>
		/// <param name="position">移動先の座標</param>
		public void SetPosition(Vector2 position)
		{
			// this.transform.positionがプレイヤーに直通するフィールドなので、それに引数として位置情報を渡す
			this.transform.position = position;
		}

		/// <summary>
		/// 他のトリガコライダと接触した時のイベント
		/// </summary>
		/// <param name="collider">接触相手のコライダ</param>
		protected void OnTriggerEnter2D(Collider2D collider)
		{
			// 死んでたら当たり判定を処理しない
			if (this.IsDead == true)
			{
				return;
			}

			// レイヤー名を取得
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			// レイヤー名に応じて処理を場合分け
			switch (otherLayerName)
			{
				// 柱または地面に触れた
				case "Pillar":
				case "Ground":
					{
						this.Damage();
						break;
					}
				default:
					{
						// Nothing to do.
						break;
					}
			}
		}

		/// <summary>
		/// ダメージを受けた時の処理
		/// </summary>
		protected void Damage()
		{
			AudioManager.Instance.PlaySE(Common.Constants.Assets.Audio.SE.boyon, 0.5f, 1.5f);

			// TODO: 必要に応じて定数化する
			float deathJumpForce = 600f;
			this.SetSpeed(Vector2.up * deathJumpForce);
			this.isDead = true;
			this.playGameScene.GameOver();
		}
	}
}