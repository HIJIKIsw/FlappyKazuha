using System.Collections.Generic;
using Flappy.Common;
using Flappy.Gimmicks;
using UnityEngine;

namespace Flappy
{
	public class PlayGameScene : SceneBase
	{
		public override string Name => "PlayGame";

		[SerializeField]
		GameObject pillarContainer;

		[SerializeField]
		PillarEmmiter pillarEmmiter;

		public void GameOver()
		{
			var pillars = this.GetAllPillars();
			foreach (var pillar in pillars)
			{
				pillar.SetSpeed(Vector2.zero);
			}
			this.pillarEmmiter.gameObject.SetActive(false);
		}

		public List<Pillar> GetAllPillars()
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