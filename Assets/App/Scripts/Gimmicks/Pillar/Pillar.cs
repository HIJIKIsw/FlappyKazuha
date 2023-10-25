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

		[SerializeField]
		private float iterationRangeY;

		private bool isMovingUp;

		private float generatedY;

		private void Start()
		{
			this.generatedY = this.transform.localPosition.y;
			this.isMovingUp = true;
		}

		private void Update()
		{
			if(this.isMovingUp == true)
			{
				if(this.transform.localPosition.y > this.generatedY + this.iterationRangeY)
				{
					this.isMovingUp = false;
				}
			}
			else
			{
				if(this.transform.localPosition.y < this.generatedY - this.iterationRangeY)
				{
					this.isMovingUp = true;
				}
			}
		}

		private void FixedUpdate()
		{
			Vector2 speed = this.rb2D.velocity;
			if(this.isMovingUp == true)
			{
				speed.y = 300f;
			}
			else
			{
				speed.y = -300f;
			}
			
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