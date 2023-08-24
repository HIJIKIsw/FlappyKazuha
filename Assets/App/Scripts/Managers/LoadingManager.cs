using UnityEngine;
using Flappy.Common;
using System;
using System.Collections.Generic;

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
		GameObject fullScreenLoadingPrefab;

		Dictionary<LoadingType, GameObject> loadingInstances = new Dictionary<LoadingType, GameObject>();

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

		public enum LoadingType
		{
			Overlay,
			FullScreen
		}

		public void Show(LoadingType type)
		{
			if( loadingInstances.ContainsKey(type) == true )
			{
				return;
			}

			var prefab = this.GetLoadingPrefabByType(type);
			if( prefab == null )
			{
				Debug.LogAssertion($"Prefab for Loading type \"{type}\" is not found.");
			}
			
			loadingInstances[type] = GameObject.Instantiate(this.GetLoadingPrefabByType(type), this.transform);
		}

		public void Hide(LoadingType type)
		{
			if( loadingInstances.ContainsKey(type) == false )
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
				case LoadingType.FullScreen:
					{
						return this.fullScreenLoadingPrefab;
					}
				default:
					{
						return null;
					}
			}
		}
	}
}