using UnityEngine;

namespace Flappy.Common
{
	public abstract class SceneBase : MonoBehaviour, IScene
	{
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