using UnityEngine;
using System;

namespace Flappy.Common
{
	public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					Type t = typeof(T);

					instance = (T)FindObjectOfType(t);
					if (instance == null)
					{
						Debug.LogError(t + " がアタッチされている GameObject が見つかりませんでした。");
					}
				}
				return instance;
			}
		}

		virtual protected void Awake()
		{
			// 他のゲームオブジェクトにアタッチされているか調べる
			if (ExistsAttachedOtherGameOcject() == true)
			{
				// アタッチされている場合は破棄する
				GameObject.Destroy(this);
			}
		}

		protected bool ExistsAttachedOtherGameOcject()
		{
			if (instance == null)
			{
				instance = this as T;
				return false;
			}
			else if (Instance == this)
			{
				return false;
			}
			return true;
		}
	}
}