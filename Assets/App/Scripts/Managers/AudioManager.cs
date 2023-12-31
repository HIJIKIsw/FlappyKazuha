using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using Flappy.Common;
using Flappy.Utility;

namespace Flappy.Manager
{
	// TODO: 一度読み込んだアセットをキャッシュしておく仕組み
	// TODO: 初回再生するまでアセットが読み込まれないので、アセットを先読みする仕組み
	// TODO: BGM用のAPI (再生、切り替え、停止など、フェード出来るとなおよい)
	public class AudioManager : SingletonMonoBehaviour<AudioManager>
	{
		/// <summary>
		/// AudioSources 初期数
		/// </summary>
		/// <remarks>足りない場合は自動で拡張する</remarks>
		[SerializeField]
		int defaultAudioInstances = 8;

		/// <summary>
		/// 同時再生最大数
		/// </summary>
		[SerializeField]
		int maxAudioInstances = 12;

		List<AudioSource> audioSources = new List<AudioSource>();

		[SerializeField, Range(0f, 1f)]
		float masterVolume = 0.5f;

		[SerializeField, Range(0f, 1f)]
		float seVolume = 0.5f;

		[SerializeField, Range(0f, 1f)]
		float bgmVolume = 0.5f;

		/// <summary>
		/// マスター音量
		/// </summary>
		public float MasterVolume
		{
			get
			{
				return this.masterVolume;
			}
			set
			{
				this.masterVolume = Mathf.Clamp01(value);
			}
		}

		/// <summary>
		/// SE 音量
		/// </summary>
		public float SeVolume
		{
			get
			{
				return this.seVolume;
			}
			set
			{
				this.seVolume = Mathf.Clamp01(value);
			}
		}

		/// <summary>
		/// BGM 音量
		/// </summary>
		public float BgmVolume
		{
			get
			{
				return this.bgmVolume;
			}
			set
			{
				this.bgmVolume = Mathf.Clamp01(value);
			}
		}

		void Start()
		{
			for (var i = 0; i < this.defaultAudioInstances; i++)
			{
				this.audioSources.Add(this.AddComponent<AudioSource>());
			}
		}

		public void PlaySE(Constants.Assets.Audio.SE se, float volume = 1f, float pitch = 1f)
		{
			// アセットを取得
			var assetAddress = AssetAddressUtility.GetAssetAddress(se);
			var asyncOperationHandle = Addressables.LoadAssetAsync<AudioClip>(assetAddress);
			asyncOperationHandle.Completed += (handle) =>
			{
				// 使用可能なAudioSourceインスタンスを取得(なければ作成)
				var audioClip = handle.Result;
				var audioSource = this.GetAvailableAudioSource();
				if (audioSource == null)
				{
					return;
				}

				// SEを再生
				audioSource.clip = audioClip;
				audioSource.volume = volume * this.masterVolume * this.seVolume;
				audioSource.pitch = pitch;
				audioSource.Play();
			};
		}

		AudioSource GetAvailableAudioSource()
		{
			var audioSource = this.audioSources.FirstOrDefault(_ => _.isPlaying == false);
			if (audioSource == null)
			{
				if (this.audioSources.Count >= this.maxAudioInstances)
				{
					return null;
				}
				audioSource = this.AddComponent<AudioSource>();
				this.audioSources.Add(audioSource);
			}
			return audioSource;
		}
	}
}