using System.Collections.Generic;
using Flappy.Common;
using Flappy.Gimmicks;
using TMPro;
using UnityEngine;

namespace Flappy
{
	public class PlayGameScene : SceneBase
	{
		/// <summary>
		/// シーン名
		/// </summary>
		public override string Name => "PlayGame";

		[SerializeField]
		private GameObject pillarContainer;

		[SerializeField]
		private PillarEmmiter pillarEmmiter;

		[SerializeField]
		private TextMeshProUGUI scoreValue;

		public bool IsProceedScoreCount { get; set; } = false;

		private float currentScore = 0f;

		private void Start()
		{
			// TODO: タップでスタート実装後はタップするまでカウント始まらないようにする
			this.IsProceedScoreCount = true;
		}

		private void Update()
		{
			if (this.IsProceedScoreCount == true)
			{
				this.currentScore += Time.deltaTime * 2f;
			}
		}

		private void FixedUpdate()
		{
			scoreValue.text = this.currentScore.ToString("F1") + " m";
		}

		/// <summary>
		/// ゲームオーバー時処理
		/// </summary>
		public void GameOver()
		{
			this.IsProceedScoreCount = false;

			var pillars = this.GetAllPillars();
			foreach (var pillar in pillars)
			{
				pillar.SetSpeed(Vector2.zero);
			}
			this.pillarEmmiter.gameObject.SetActive(false);
		}

		/// <summary>
		/// 全ての柱を取得
		/// </summary>
		private List<Pillar> GetAllPillars()
		{
			var pillars = new List<Pillar>();
			var pillarCount = this.pillarContainer.transform.childCount;
			for (int i = 0; i < pillarCount; i++)
			{
				var pillar = this.pillarContainer.transform.GetChild(i).GetComponent<Pillar>();
				if (pillar != null)
				{
					pillars.Add(pillar);
				}
			}
			return pillars;
		}
	}
}