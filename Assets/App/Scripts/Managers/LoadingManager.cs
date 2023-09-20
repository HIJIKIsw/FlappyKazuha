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
		Loading1 loading1Prefab;
		Loading1 loading1Instance;

		[SerializeField]
		Loading2 loading2Prefab;
		Loading2 loading2Instance;

		[SerializeField]
		Loading3 loading3Prefab;
		Loading3 loading3Instance;

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
				return this.loading1Instance != null || this.loading2Instance != null;
			}
		}

		/// <summary>
		/// オーバーレイロード画面を表示
		/// </summary>
		public void ShowOverlay()
		{
			if (this.loading1Instance != null)
			{
				return;
			}

			this.loading1Instance = GameObject.Instantiate(this.loading1Prefab, this.transform);
			this.loading1Instance.Show();
		}

		/// <summary>
		/// オーバーレイロード画面を非表示
		/// </summary>
		public void HideOverlay()
		{
			if (this.loading1Instance == null)
			{
				return;
			}

			this.loading1Instance.Hide();
			this.loading1Instance = null;
		}

		/// <summary>
		/// フルスクリーンロード画面を表示
		/// </summary>
		/// <param name="tasksCount">ロード完了までのタスクの個数</param>
		/// <param name="onBeginLoad">フェードイン完了後に実行するアクション</param>
		/// <remarks>フルスクリーンロードは直接非表示にできず、タスクが完了するたびに CompleteTask を呼び出して、LoadingManager 側で非表示にする。</remarks>
		public void ShowFullscreen(int tasksCount, UnityAction onBeginLoad = null, UnityAction onCompleteLoad = null)
		{
			if (this.loading2Instance != null)
			{
				// 複数箇所から同時にロード画面を使用する場合はすべてのタスクを合計する
				this.tasksCount += tasksCount;
				return;
			}

			this.tasksCount = tasksCount;
			this.completedTasks = 0;

			this.loading2Instance = GameObject.Instantiate(this.loading2Prefab, this.transform);
			this.loading2Instance.Show(onBeginLoad: onBeginLoad, onCompleteLoad: onCompleteLoad);
		}

		/// <summary>
		/// タスクが完了したら呼び出す
		/// </summary>
		/// <param name="count">完了したタスクの個数</param>
		public void CompleteTask(int count = 1)
		{
			if (count < 1 || this.loading2Instance == null)
			{
				return;
			}

			this.completedTasks += count;
			var progress = (float)this.completedTasks / (float)this.tasksCount;
			this.loading2Instance.SetProgress(progress);
			if (progress >= 1f)
			{
				this.loading2Instance = null;
			}
		}
	}
}