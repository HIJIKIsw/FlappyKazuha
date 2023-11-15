using Flappy.Common;
using Flappy.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Flappy
{
	public class HomeScene : SceneBase
	{
		public override string Name => "Home";

		/// <summary>
		/// CharacterListオブジェクト
		/// </summary>
		[SerializeField]
		private TMP_Dropdown characterList;

		[SerializeField]
		private Image characterImage;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Start()
		{
			// キャラ変更時に呼び出されるイベントを登録
			this.characterList.onValueChanged.AddListener(this.OnChangeCharacter);
		}

		/// <summary>
		/// キャラ変更時のイベント
		/// </summary>
		/// <param name="characterIndex">選択されたキャラクターの番号</param>
		private void OnChangeCharacter(int characterIndex)
		{
			// TODO: 固定でnullをセットしているところを、characterIndexの値に応じて変える (対応するキャラクターのSpriteをセットする)
			this.characterImage.sprite = null;
		}

		/// <summary>
		/// Homeシーンで「開始」ボタンを押した時に実行される
		/// </summary>
		public void StartPlayGame()
		{
			// シーンに渡すパラメータを生成
			SceneParameter parameter = new SceneParameter();

			// 選択されたキャラクターに応じてパラメータに値をセット
			switch (characterList.value)
			{
				case 0:
					{
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
						break;
					}
				case 1:
					{
						Debug.Log(parameter);
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Wanderer);
						break;
					}
				default:
					{
						Debug.Log(parameter);
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
						break;
					}
			}

			// PlayGameSceneにパラメータを渡す
			SceneManager.Instance.Load<PlayGameScene>(parameter);
		}

	}


}