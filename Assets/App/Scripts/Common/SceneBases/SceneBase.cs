using UnityEngine;
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
		/// <param name="parameter">シーンに渡すパラメータ</param>
		public virtual void Initialize(SceneParameter parameter = null)
		{
			// TODO: 初期化が完了したことをSceneManagerに知らせるデリゲートを持たせる
			this.parameter = parameter ?? new SceneParameter();
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