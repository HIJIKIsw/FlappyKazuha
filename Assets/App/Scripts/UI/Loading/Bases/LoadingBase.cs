using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Flappy.Common
{
	/// <summary>
	/// ローディング表示の基底クラス
	/// </summary>
	public abstract class LoadingBase : MonoBehaviour, ILoading
	{
		/// <summary>
		/// ロード開始時(フェードイン完了後)に実行するアクション
		/// </summary>
		protected UnityAction onBeginLoad;

		/// <summary>
		/// ロード完了時(フェードイン開始前)に実行するアクション
		/// </summary>
		protected UnityAction onCompleteLoad;

		/// <summary>
		/// ロード画面を表示する
		/// </summary>
		/// <param name="onBeginLoad">ロード開始時のアクション</param>
		/// <param name="onCompleteLoad">ロード完了時のアクション</param>
		/// <remarks>フェードを使用するローディングタイプではオーバーライドして実装する</remarks>
		public virtual void Show(UnityAction onBeginLoad, UnityAction onCompleteLoad)
		{
			// ロード開始・完了時のアクションをセット
			this.onBeginLoad = onBeginLoad;
			this.onCompleteLoad = onCompleteLoad;

			// ゲームオブジェクトを有効にする
			this.gameObject.SetActive(true);

			// ロード開始時のアクションを実行する
			this.onBeginLoad?.Invoke();
		}

		/// <summary>
		/// ロード画面を非表示にする
		/// </summary>
		/// <remarks>外部からは呼び出せず、SetProgress()を経由して呼び出す</remarks>
		/// <remarks>フェードを使用するローディングタイプではオーバーライドして実装する</remarks>
		protected virtual void Hide()
		{
			// ロード完了時のアクションを実行する
			this.onCompleteLoad?.Invoke();

			// ゲームオブジェクトを削除する
			GameObject.Destroy(this.gameObject);
		}

		/// <summary>
		/// ロード開始時のアクションを追加
		/// </summary>
		/// <param name="onBeginLoad">ロード開始時のアクション</param>
		public virtual void SetOnBeginLoad(UnityAction onBeginLoad)
		{
			if (onBeginLoad == null)
			{
				return;
			}
			this.onBeginLoad += onBeginLoad;
		}

		/// <summary>
		/// ロード完了時のアクションを追加
		/// </summary>
		/// <param name="onCompleteLoad">ロード完了時のアクション</param>
		public virtual void SetOnCompleteLoad(UnityAction onCompleteLoad)
		{
			if (onCompleteLoad == null)
			{
				return;
			}
			this.onCompleteLoad += onCompleteLoad;
		}

		/// <summary>
		/// ロード進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		/// <remarks>プログレスバーが存在するローディングタイプではオーバーライドしてバーの表示反映を実装する</remarks>
		public virtual void SetProgress(float progress)
		{
			if ( progress >= 1f )
			{
				this.Hide();
			}
		}
	}
}