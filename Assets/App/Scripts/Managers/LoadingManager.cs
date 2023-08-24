using UnityEngine;
using Flappy.Common;
using System;
using System.Collections.Generic;
using Flappy.UI;

namespace Flappy.Manager
{
	/// <summary>
	/// ロード画面マネージャ
	/// </summary>
	public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
	{
		[SerializeField]
		GameObject overlayLoadingPrefab;

		[SerializeField]
		GameObject fullscreenLoadingPrefab;

		Dictionary<LoadingType, GameObject> loadingInstances = new Dictionary<LoadingType, GameObject>();

		// フルスクリーンロード画面の進捗状況管理用
		int tasksCount;
		int completedTasks;
		Loading2 loading2;

		/// <summary>
		/// ロード中か
		/// </summary>
		public bool IsLoading
		{
			get
			{
				return this.loadingInstances.Count > 0;
			}
		}

		enum LoadingType
		{
			Overlay,
			Fullscreen
		}

		void Show(LoadingType type)
		{
			if (loadingInstances.ContainsKey(type) == true)
			{
				return;
			}

			var prefab = this.GetLoadingPrefabByType(type);
			if (prefab == null)
			{
				Debug.LogAssertion($"Prefab for Loading type \"{type}\" is not found.");
			}

			loadingInstances[type] = GameObject.Instantiate(this.GetLoadingPrefabByType(type), this.transform);
		}

		void Hide(LoadingType type)
		{
			if (loadingInstances.ContainsKey(type) == false)
			{
				return;
			}

			GameObject.Destroy(loadingInstances[type]);
			loadingInstances.Remove(type);
		}

		GameObject GetLoadingPrefabByType(LoadingType type)
		{
			switch (type)
			{
				case LoadingType.Overlay:
					{
						return this.overlayLoadingPrefab;
					}
				case LoadingType.Fullscreen:
					{
						return this.fullscreenLoadingPrefab;
					}
				default:
					{
						return null;
					}
			}
		}

		/// <summary>
		/// オーバーレイロード画面を表示
		/// </summary>
		public void ShowOverlay()
		{
			this.Show(LoadingType.Overlay);
		}

		/// <summary>
		/// オーバーレイロード画面を非表示
		/// </summary>
		public void HideOverlay()
		{
			this.Hide(LoadingType.Overlay);
		}

		/// <summary>
		/// フルスクリーンロード画面を表示
		/// </summary>
		/// <param name="tasksCount">ロード完了までのタスクの個数</param>
		/// <remarks>フルスクリーンロードは直接非表示にできず、タスクが完了するたびに CompleteTask を呼び出して、LoadingManager 側で非表示にする。</remarks>
		public void ShowFullscreen(int tasksCount)
		{
			this.tasksCount = tasksCount;
			this.completedTasks = 0;

			this.Show(LoadingType.Fullscreen);
			this.loading2 = this.loadingInstances[LoadingType.Fullscreen].GetComponent<Loading2>();
		}

		/// <summary>
		/// タスクが完了したら呼び出す
		/// </summary>
		/// <param name="count">完了したタスクの個数</param>
		public void CompleteTask(int count = 1)
		{
			if (count < 1 || this.loadingInstances.ContainsKey(LoadingType.Fullscreen) == false)
			{
				return;
			}

			this.completedTasks += count;

			if (this.completedTasks >= this.tasksCount)
			{
				this.Hide(LoadingType.Fullscreen);
				return;
			}

			this.loading2.SetProgress((float)this.completedTasks / (float)this.tasksCount);
		}
	}
}