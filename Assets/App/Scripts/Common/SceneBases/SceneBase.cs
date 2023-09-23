using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Flappy.Common
{
	public abstract class SceneBase : MonoBehaviour, IScene
	{
		/// <summary>
		/// Unity シーンオブジェクト
		/// </summary>
		public Scene Scene
		{
			get
			{
				return this.gameObject.scene;
			}
		}

		/// <summary>
		/// シーンファイル名
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// シーンに渡されたパラメータ
		/// </summary>
		public SceneParameter parameter;

		/// <summary>
		/// シーンを初期化
		/// </summary>
		/// <param name="notification">初期化結果を通知するデリゲート</param>
		/// <param name="parameter">シーンに渡すパラメータ</param>
		public virtual void Initialize(UnityAction<SceneState> notification, SceneParameter parameter = null)
		{
			this.parameter = parameter ?? new SceneParameter();

			// 初期化が完了したことをSceneManagerに知らせる
			notification(SceneState.Success);
		}

		/// <summary>
		/// シーンのアクティブ状態を設定
		/// </summary>
		public virtual void SetActive(bool value)
		{
			this.gameObject.SetActive(value);
		}
	}
}