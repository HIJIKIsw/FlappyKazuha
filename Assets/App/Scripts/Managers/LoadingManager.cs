using UnityEngine;
using UnityEngine.Events;
using Flappy.Common;
using Flappy.UI;

namespace Flappy.Manager
{
	/// <summary>
	/// ロード画面マネージャ
	/// </summary>
	public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
	{
		[SerializeField]
		Loading1 overlayLoadingPrefab;
		Loading1 overlayLoadingInstance;

		[SerializeField]
		Loading2 fullscreenLoadingPrefab;
		Loading2 fullscreenLoadingInstance;

		// フルスクリーンロード画面の進捗状況管理用
		int tasksCount;
		int completedTasks;

		/// <summary>
		/// ロード中か
		/// </summary>
		public bool IsLoading
		{
			get
			{
				return this.overlayLoadingInstance != null || this.fullscreenLoadingInstance != null;
			}
		}

		/// <summary>
		/// オーバーレイロード画面を表示
		/// </summary>
		public void ShowOverlay()
		{
			if (this.overlayLoadingInstance != null)
			{
				return;
			}

			this.overlayLoadingInstance = GameObject.Instantiate(this.overlayLoadingPrefab, this.transform);
			this.overlayLoadingInstance.Show();
		}

		/// <summary>
		/// オーバーレイロード画面を非表示
		/// </summary>
		public void HideOverlay()
		{
			if (this.overlayLoadingInstance == null)
			{
				return;
			}

			this.overlayLoadingInstance.Hide();
			this.overlayLoadingInstance = null;
		}

		/// <summary>
		/// フルスクリーンロード画面を表示
		/// </summary>
		/// <param name="tasksCount">ロード完了までのタスクの個数</param>
		/// <param name="onBeginLoad">フェードイン完了後に実行するアクション</param>
		/// <remarks>フルスクリーンロードは直接非表示にできず、タスクが完了するたびに CompleteTask を呼び出して、LoadingManager 側で非表示にする。</remarks>
		public void ShowFullscreen(int tasksCount, UnityAction onBeginLoad = null, UnityAction onCompleteLoad = null)
		{
			if (this.fullscreenLoadingInstance != null)
			{
				// 複数箇所から同時にロード画面を使用する場合はすべてのタスクを合計する
				this.tasksCount += tasksCount;
				return;
			}

			this.tasksCount = tasksCount;
			this.completedTasks = 0;

			this.fullscreenLoadingInstance = GameObject.Instantiate(this.fullscreenLoadingPrefab, this.transform);
			this.fullscreenLoadingInstance.Show(onBeginLoad: onBeginLoad, onCompleteLoad: onCompleteLoad);
		}

		/// <summary>
		/// タスクが完了したら呼び出す
		/// </summary>
		/// <param name="count">完了したタスクの個数</param>
		public void CompleteTask(int count = 1)
		{
			if (count < 1 || this.fullscreenLoadingInstance == null)
			{
				return;
			}

			this.completedTasks += count;
			var progress = (float)this.completedTasks / (float)this.tasksCount;
			this.fullscreenLoadingInstance.SetProgress(progress);
			if (progress >= 1f)
			{
				this.fullscreenLoadingInstance = null;
			}
		}
	}
}