using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Flappy.Common;
using Flappy.Manager;
using Unity.VisualScripting;
using TMPro;
using System.Collections.Generic;

namespace Flappy.UI
{
	/// <summary>
	/// フルスクリーンロード画面
	/// </summary>
	public sealed class Loading2 : FullscreenLoadingBase
	{
		/// <summary>
		/// プログレスバーのImageオブジェクト
		/// </summary>
		[SerializeField]
		Image progressBarFill;

		[SerializeField]
		Image loadingBg;

		[SerializeField]
		Image progressBarLineLeft;

		[SerializeField]
		Image progressBarLineRight;

		[SerializeField]
		TextMeshProUGUI tips;

		[SerializeField]
		Image progressBarBase;

		int currentTipsNumber;

		float tipsChangeInterval;

		List<string> tipsList = new List<string>();

		protected override float fadeTime => 0.4f;
		protected override float minDisplayTime => 1.5f;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// 効果音を再生
			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku1, 0.3f, 0.8f);
			AudioManager.Instance.PlaySE(Constants.Assets.Audio.SE.kaifuku2, 0.7f, 2.5f);

			// 時間帯によって色を変える
			int hour = System.DateTime.Now.Hour;
			if ( hour <= 7 || hour >= 19 )
			{
				this.loadingBg.color = new Color32(26,28,32,255);
				this.progressBarLineLeft.color = this.progressBarLineRight.color = new Color32(56,58,62,255);
				this.progressBarBase.color = new Color32(48,50,54,255);
				this.tips.color = new Color32(207,189,144,255);
				this.progressBarFill.color = new Color32(234,228,213,255);
			}

			this.InitTipsList();
			this.ChangeTips();
		}

		/// <summary>
		/// Tipsリスト
		/// </summary>
		private void InitTipsList()
		{
			this.tipsList.Add("<size=22>胡桃</size>\n炎アタッカー");
			this.tipsList.Add("<size=22>ヨォーヨ</size>\n水アタッカー");
			this.tipsList.Add("<size=22>放浪者</size>\n笠っち");
			this.tipsList.Add("<size=22>アルレッキーノ</size>\n気になる");
			this.tipsList.Add("<size=22>フォンテーヌ</size>\n地理の性質から言えば、フォンテーヌの地上大湖は「湖」であるが、フォンテーヌの人々はそれを「海」と呼んでいる。");
		}

		/// <summary>
		/// 左クリック時にTipsテキストを変更する
		/// </summary>
		protected override void Update()
		{
			base.Update();

			if (Input.GetMouseButtonDown(0))
			{
				this.ChangeTips();
				this.tipsChangeInterval = 0;
			}

			this.tipsChangeInterval += Time.deltaTime;
			if (this.tipsChangeInterval >= 5)
			{
				this.ChangeTips();
				this.tipsChangeInterval = 0;
			}
		}

		/// <summary>
		/// Tipsテキストを変更する関数
		/// </summary>
		private void ChangeTips()
		{
			int listCount = tipsList.Count;
			int rnd = Random.Range(0, listCount);

			if (rnd == this.currentTipsNumber)
			{
				if (rnd == listCount-1)
				{
					rnd--;
				}
				else
				{
					rnd++;
				}
			}

			this.tips.text = this.tipsList[rnd];
			this.currentTipsNumber = rnd;
		}

		/// <summary>
		/// プログレスバーの進捗をセット
		/// </summary>
		/// <param name="progress">進捗状況: 0f-1f</param>
		/// TODO: バーを巻き戻すようなことができないようにする
		public override void SetProgress(float progress)
		{
			progress = Mathf.Clamp(progress, 0f, 1f);
			this.progressBarFill.DOFillAmount(progress, 0.2f).OnComplete(() =>
			{
				if (progress >= 1f)
				{
					this.Hide();
				}
			});
		}
	}
}