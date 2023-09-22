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
		/// <summary>
		/// Loading1 プレハブ
		/// </summary>
		[SerializeField]
		private Loading1 loading1Prefab;

		/// <summary>
		/// Loading2 プレハブ
		/// </summary>
		[SerializeField]
		private Loading2 loading2Prefab;

		/// <summary>
		/// Loading3 プレハブ
		/// </summary>
		[SerializeField]
		private Loading3 loading3Prefab;

		/// <summary>
		/// ロード画面インスタンス
		/// </summary>
		private LoadingBase loadingInstance;

		/// <summary>
		/// ロード完了までの合計タスク数
		/// </summary>
		private int tasksCount;

		/// <summary>
		/// 完了済タスク数
		/// </summary>
		private int completedTasks;

		/// <summary>
		/// ロード中か
		/// </summary>
		public bool IsLoading
		{
			get
			{
				return this.loadingInstance != null;
			}
		}

		/// <summary>
		/// ローディング表示タイプ
		/// </summary>
		public enum Types
		{
			Overlay,
			Fullscreen,
			FullscreenWithoutProgressbar
		}

		/// <summary>
		/// ロード画面を表示
		/// </summary>
		/// <param name="type">表示するロード画面タイプ</param>
		/// <param name="tasksCount">ロード完了までのタスクの個数</param>
		/// <param name="onBeginLoad">ロード開始時に実行するアクション</param>
		/// <param name="onCompleteLoad">ロード完了時に実行するアクション</param>
		/// <remarks>
		/// 通常は既存のロード画面がある状態でこのメソッドを呼ばないように注意しながら実装する想定。
		/// 仮に、既に表示中のロードが存在する場合はタスクのカウンターだけが足されてロード画面は既存のもののまま。
		/// onBeginLoadは即座に実行され、onCompleteLoadは既存のロード完了時アクションに追加される。
		/// onBeginLoadは即座に実行されるので、ロード画面のフェードインが終わっているかどうかを考慮しない点にも注意。
		/// </remarks>
		public void Show(Types type = Types.Overlay, int tasksCount = 1, UnityAction onBeginLoad = null, UnityAction onCompleteLoad = null)
		{
			// 不正なタスク数
			if (tasksCount < 1)
			{
				Debug.LogAssertion("指定されたタスク数が不正なためロードをキャンセルしました。");
				return;
			}

			if (this.IsLoading == true)
			{
				// ロード開始時アクションは即座に実行
				onBeginLoad?.Invoke();

				// ロード終了時アクションは既存のロード画面に追加する
				if (onCompleteLoad != null)
				{
					this.loadingInstance.SetOnCompleteLoad(onCompleteLoad);
				}

				// ロード完了までのタスク数を加算する
				this.tasksCount += tasksCount;

				// 既にロード中の場合は新たにロード画面を表示しない
				return;
			}

			// タスク状況をリセット
			this.tasksCount = tasksCount;
			this.completedTasks = 0;

			// ローディングタイプによって読み込むプレハブを場合分け
			switch (type)
			{
				case Types.Overlay:
					this.loadingInstance = GameObject.Instantiate(this.loading1Prefab, this.transform);
					break;
				case Types.Fullscreen:
					this.loadingInstance = GameObject.Instantiate(this.loading2Prefab, this.transform);
					break;
				case Types.FullscreenWithoutProgressbar:
					this.loadingInstance = GameObject.Instantiate(this.loading3Prefab, this.transform);
					break;
			}

			// ロード画面を表示
			this.loadingInstance.Show(onBeginLoad, onCompleteLoad);
		}

		/// <summary>
		/// タスクが1つ完了するごとに1回呼び出す
		/// </summary>
		public void CompleteTask()
		{
			// ロード中でなければ無視する
			if (this.IsLoading == false)
			{
				Debug.Log("ロード中でないため無視しました。");
				return;
			}

			// 完了タスク数を1つ進める
			this.completedTasks++;

			// 進捗をセット
			var progress = Mathf.Clamp01((float)this.completedTasks / (float)this.tasksCount);
			this.loadingInstance.SetProgress(progress);

			// ロード完了したらインスタンスへの参照を切る
			if (progress >= 1f)
			{
				this.loadingInstance = null;
			}
		}

		/// <summary>
		/// ロードを強制的に完了させる
		/// </summary>
		/// <remarks>扱いに注意。通常は使用しないが、通信エラー時などにロード画面を非表示にしたりするのに使う。</remarks>
		public void CompleteDirty()
		{
			// ロード中でなければ無視する
			if (this.IsLoading == false)
			{
				Debug.Log("ロード中でないため無視しました。");
				return;
			}

			// タスク状況をリセット
			this.tasksCount = 0;
			this.completedTasks = 0;

			// ロードを強制的に完了してインスタンスへの参照を切る
			this.loadingInstance.SetProgress(1f);
			this.loadingInstance = null;
		}
	}
}