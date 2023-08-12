using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Flappy
{
	public class PlayerController : MonoBehaviour
	{
		Rigidbody2D rigidbody2D;

		/// <summary>
		/// ジャンプ初速
		/// </summary>
		[SerializeField]
		float jumpForce;

		bool isJump = false;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start()
		{
			this.rigidbody2D = this.GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.isJump = true;
			}
		}

		void FixedUpdate()
		{
			if (this.isJump)
			{
				this.rigidbody2D.velocity = Vector2.up * jumpForce;
				this.isJump = false;
			}

		}
	}

}
