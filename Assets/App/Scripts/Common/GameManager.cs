using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
	/// <summary>
	///  ゲームマネージャ
	/// </summary>
	/// <remarks>シングルトンクラス</remarks>
	public class GameManager : MonoBehaviour
	{
		/// <summary>
		/// インスタンス
		/// </summary>
		static GameManager instance;
		
		/// <summary>
		/// シングルトンインスタンス
		/// </summary>
		public static GameManager Instance
		{
			get
			{
				if( GameManager.instance == null )
				{
					GameManager.instance = new GameManager();
				}
				return GameManager.instance;
			}
		}

		/// <summary>
		/// コンストラクタは隠蔽する
		/// </summary>
		GameManager()
		{
			// Nothing to do.
		}

		/// <summary>
		/// 原石の所持数
		/// </summary>
		/// TODO: 所持品クラスみたいなものを作ってそっちにまとめる (原石、ネームプレート、称号など)
		public int PrimogemCount { get; set; } = 0;
	}
}

