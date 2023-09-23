using UnityEngine;

namespace Flappy
{
	/// <summary>
	/// 万葉
	/// </summary>
	public class Kazuha : PlayerBase
	{
		/// <summary>
		/// ジャンプ初速
		/// </summary>
		[SerializeField]
		private float jumpForce;

		/// <summary>
		/// ジャンプが入力されたらtrueになる、ジャンプ実行時にfalseにする
		/// </summary>
		private bool isJump = false;

		/// <summary>
		/// 更新 (1フレーム)
		/// </summary>
		private void Update()
		{
			// 画面クリック時にジャンプフラグをtrueにする
			if (Input.GetMouseButtonDown(0))
			{
				this.isJump = true;
			}
		}

		/// <summary>
		/// 更新 (固定時間)
		/// </summary>
		/// <remarks>物理演算に関わる処理はこっちでやる</remarks>
		private void FixedUpdate()
		{
			// TODO: いちいち操作ごとに死亡フラグを確認するのは面倒なのでイケてる方法考える
			//       入力に関わる処理をメソッド抽出して、IsDeadがfalseの間しかそのメソッドを呼ばないなど
			if (this.isJump && this.IsDead == false)
			{
				this.rb2D.velocity = Vector2.up * jumpForce;
				this.isJump = false;
			}
		}
	}
}