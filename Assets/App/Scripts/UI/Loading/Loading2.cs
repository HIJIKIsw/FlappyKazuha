using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Flappy.Common;
using Flappy.Manager;
using Unity.VisualScripting;
using TMPro;

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

			//Tipsテキスト内容をセット
			SetTips();
		}

		/// <summary>
		/// 左クリック時にTipsテキストを変更する
		/// </summary>
		protected override void Update()
		{
			base.Update();

			//左クリックを受け付ける
			if (Input.GetMouseButtonDown(0))
			{
				SetTips();
			}
		}

		/// <summary>
		/// Tipsテキストを変更する関数
		/// </summary>
		private void SetTips()
		{
			int rnd = Random.Range(1, 4);

			if (rnd == currentTipsNumber)
			{
				if (rnd == 3)
				{
					rnd--;
				}
				else
				{
					rnd++;
				}
			}

			switch (rnd)
			{
				case 1:
					tips.text = "<size=22>フォンテーヌ</size>\n地理の性質から言えば、フォンテーヌの地上大湖は「湖」であるが、フォンテーヌの人々はそれを「海」と呼んでいる。";
					break;
				case 2:
					tips.text = "<size=22>モンド</size>\nかつて、この土地にはデカラビアンという名の魔神が存在していた。\n\n大陵の北東にある自由の都。\n山と荒野の間で、自由の風が蒲公英の種と共にシードル湖を渡り、湖の中心にあるモンド城に風神の祝福と恩恵をもたらす。";
					break;
				case 3:
					tips.text = "<size=22>璃月</size>\n大陸の束にある豊かな港湾地域。\nそぴえ立つ山と石の林、広い平原と生き生きとした川など、豊富な地形を有する。\n\n璃月の土地には古代の遣跡が点在している。そのうち、極めて高い技術で作られた巨大弩砲「帰終機」が存在する。";
					break;
				default:
					tips.text = "<size=22></size>\n";
					break;
			}
			currentTipsNumber = rnd;
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