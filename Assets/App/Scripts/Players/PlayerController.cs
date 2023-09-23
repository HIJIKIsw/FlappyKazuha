using Flappy.Manager;
using UnityEngine;

namespace Flappy
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D rb2D;

		/// <summary>
		/// ジャンプ初速
		/// </summary>
		[SerializeField]
		float jumpForce;

		[SerializeField]
		PlayGameScene playGameScene;

		bool isJump = false;

		bool isDead = false;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start()
		{
			this.rb2D = this.GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.isJump = true;
			}
		}

		void FixedUpdate()
		{
			// TODO: いちいち操作ごとに死亡フラグを確認するのは面倒なのでイケてる方法考える
			if (this.isJump && this.isDead == false)
			{
				this.rb2D.velocity = Vector2.up * jumpForce;
				this.isJump = false;
			}
		}

		void SetSpeed(Vector2 speed)
		{
			this.rb2D.velocity = speed;
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			var otherLayer = collider.gameObject.layer;
			var otherLayerName = LayerMask.LayerToName(otherLayer);

			switch (otherLayerName)
			{
				case "Pillar":
					{
						this.OnTriggerPillar();
						break;
					}
				default:
					{
						// Nothing to do.
						break;
					}
			}
		}

		void OnTriggerPillar()
		{
			// TODO: 死亡フラグで扱うよりPillar側の BoxCollider を無効にしたほうがよさそう
			if (isDead == true)
			{
				return;
			}

			AudioManager.Instance.PlaySE(Common.Constants.Assets.Audio.SE.boyon, 0.5f, 1.5f);

			this.isDead = true;
			this.playGameScene.GameOver();
			this.SetSpeed(Vector2.up * this.jumpForce);
		}

		public void SetPosition(Vector2 position)
		{
			// this.transform.positionがプレイヤーに直通するフィールドなので、それに引数として位置情報を渡す
			this.transform.position = position;
		}
	}
}
