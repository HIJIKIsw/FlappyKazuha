using UnityEngine;

namespace Flappy
{
	/// <summary>
	/// 放浪者
	/// </summary>
	public class Wanderer : PlayerBase
	{
		/// <summary>
		/// 上方向へ加える力
		/// </summary>
		[SerializeField]
		private float addForceY;

		/// <summary>
		/// 上方向へ力を加えるか
		/// </summary>
		private bool isApplyForceY = false;

		/// <summary>
		/// 更新 (1フレーム)
		/// </summary>
		private void Update()
		{
			// 画面クリック時にフラグをtrueにする
			if (Input.GetMouseButton(0))
			{
				this.isApplyForceY = true;
			}
			// 画面クリックしてない時にフラグをfalseにする
			else
			{
				this.isApplyForceY = false;
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
			if (this.isApplyForceY && this.IsDead == false)
			{
				this.rb2D.AddForce(Vector2.up * this.addForceY, ForceMode2D.Force);
			}
		}
	}
}