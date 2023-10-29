using UnityEngine;

namespace Flappy.Gimmicks
{
	/// <summary>
	/// 柱
	/// </summary>
	public class Pillar : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody2D rb2D;

		/// <summary>
		/// 往復幅
		/// </summary>
		[SerializeField]
		private float iterationRangeY;

		/// <summary>
		/// 往復速度
		/// </summary>
		[SerializeField]
		private float iterationSpeedY;

		/// <summary>
		/// 上に動いているか
		/// </summary>
		private bool isMovingUp;

		/// <summary>
		/// 生成したY座標
		/// </summary>
		private float generatedY;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// 生成時点のY座標を保持しておく
			this.generatedY = this.transform.localPosition.y;

			// 上下どちらに動くかランダムで決定
			if (Random.Range(0, 2) == 0)
			{
				this.isMovingUp = false;
			}
			else
			{
				this.isMovingUp = true;
			}

			// 往復幅をランダムで決定
			this.iterationRangeY = Random.Range(0f, 100f);

			// 往復速度をランダムで決定
			this.iterationSpeedY = Random.Range(0f, 50f);

			// 往復速度と幅が特定の値以下の場合は0にする
			if(this.iterationRangeY < 10f || this.iterationSpeedY < 10f)
			{
				this.iterationRangeY = 0f;
				this.iterationSpeedY = 0f;
			}
		}

		/// <summary>
		/// 更新 (1フレーム)
		/// </summary>
		private void Update()
		{
			// 往復範囲の応じて移動方向の切り替え
			if (this.isMovingUp == true)
			{
				// 往復範囲の判定
				if (this.transform.localPosition.y > this.generatedY + this.iterationRangeY)
				{
					this.isMovingUp = false;
				}
			}
			else
			{
				// 往復範囲の判定
				if (this.transform.localPosition.y < this.generatedY - this.iterationRangeY)
				{
					this.isMovingUp = true;
				}
			}
		}
		
		/// <summary>
		/// 更新 (固定時間)
		/// </summary>
		/// <remarks>物理演算に関わる処理はこっちでやる</remarks>
		private void FixedUpdate()
		{
			// 自身の現在速度を取得
			Vector2 speed = this.rb2D.velocity;

			// 移動方向に応じたY軸の移動速度(+/-)を求める
			if (this.isMovingUp == true)
			{
				speed.y = iterationSpeedY;
			}
			else
			{
				speed.y = -iterationSpeedY;
			}

			// 移動速度を設定
			this.SetSpeed(speed);
		}

		/// <summary>
		/// 柱を消す処理
		/// </summary>
		public void Remove()
		{
			GameObject.Destroy(this.gameObject);
		}

		/// <summary>
		/// 移動速度を設定する
		/// </summary>
		public void SetSpeed(Vector2 speed)
		{
			this.rb2D.velocity = speed;
		}
	}
}