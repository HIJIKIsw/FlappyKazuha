using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Flappy.Manager;
using Unity.VisualScripting;

namespace Flappy.PlayGame
{
	/// <summary>
	/// スコアゲージ
	/// </summary>
	public class ScoreGauge : MonoBehaviour
	{
		/// <summary>
		/// 現在スコアテキスト
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI currentScoreText;

		/// <summary>
		/// 最高スコアテキスト
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI bestScoreText;

		/// <summary>
		/// CurrentScoreMaskのImageコンポーネント
		/// </summary>
		[SerializeField]
		private Image currentScoreMask;

		/// <summary>
		/// BestScoreMaskのImageコンポーネント
		/// </summary>
		[SerializeField]
		private Image bestScoreMask;

		/// <summary>
		/// BestIconのRectTransformコンポーネント
		/// </summary>
		[SerializeField]
		private RectTransform bestIcon;

		/// <summary>
		/// ゲージの横幅
		/// </summary>
		private float gaugeWidth;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// ゲージの横幅を取得
			this.gaugeWidth = (this.transform as RectTransform).sizeDelta.x;

			if (GameManager.Instance.IsRecordedBestScore == true)
			{
				// 最高スコア表示は初期化時にセット
				this.SetBestScoreText(GameManager.Instance.BestScore);
			}
			else
			{
				// 最高スコアが0の時はゲージを緑に塗りつぶしておく
				this.currentScoreMask.fillAmount = 1f;

				// 最高スコアが0の時は最高スコアアイコンを非表示にする
				this.bestIcon.gameObject.SetActive(false);

				// 最高スコアが0の時は固定テキストを表示する
				this.bestScoreText.text = "記録なし";
			}
		}

		/// <summary>
		/// 現在スコアをゲージに反映する
		/// </summary>
		/// <param name="currentScore">現在スコア</param>
		public void SetCurrentScore(float currentScore)
		{
			// ゲージ表示を更新
			this.UpdateDisplay(currentScore);

			// 現在スコアテキストをセット
			this.SetCurrentScoreText(currentScore);
		}

		/// <summary>
		/// ゲージ表示を更新
		/// </summary>
		/// <param name="currentScore">現在スコア</param>
		private void UpdateDisplay(float currentScore)
		{
			// 最高スコアがない時は何もしない
			if (GameManager.Instance.IsRecordedBestScore == false)
			{
				return;
			}

			// スコアを少数第一位まで丸める
			currentScore = GameManager.Instance.RoundScore(currentScore);

			// 最高スコアに対する現在スコアの割合
			var currentToBestRatio = currentScore / GameManager.Instance.BestScore;

			if (currentScore <= GameManager.Instance.BestScore)
			{
				// 最高スコアに到達するまでのゲージ塗りつぶし
				this.currentScoreMask.fillAmount = Mathf.Clamp01(currentToBestRatio);
			}
			else
			{
				// 最高スコアを超えてからのゲージ塗りつぶし
				var fillAmount = 1f - (1f / currentToBestRatio);
				this.bestScoreMask.fillAmount = fillAmount;

				// 最高スコアアイコン座標を更新
				this.UpdateBestIconPosition(fillAmount);
			}
		}

		/// <summary>
		/// 最高スコアアイコンの位置を更新
		/// </summary>
		/// <param name="positionRatioX">横幅に対する位置割合 (0-1f)</param>
		private void UpdateBestIconPosition(float positionRatioX)
		{
			var newPosition = this.bestIcon.anchoredPosition;
			newPosition.x = -this.gaugeWidth * positionRatioX;
			this.bestIcon.anchoredPosition = newPosition;
		}

		/// <summary>
		/// 現在スコアテキストをセット
		/// </summary>
		/// <param name="currentScore">現在スコア</param>
		private void SetCurrentScoreText(float currentScore)
		{
			this.currentScoreText.text = GameManager.Instance.ScoreToText(currentScore);
		}

		/// <summary>
		/// 最高スコアテキストをセット
		/// </summary>
		/// <param name="bestScore">最高スコア</param>
		private void SetBestScoreText(float bestScore)
		{
			this.bestScoreText.text = GameManager.Instance.ScoreToText(bestScore);
		}
	}
}