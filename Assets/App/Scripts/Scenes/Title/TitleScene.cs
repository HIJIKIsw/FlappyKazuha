using Flappy.Common;
using Flappy.Manager;
using TMPro;
using UnityEngine;

namespace Flappy
{
	public class TitleScene : SceneBase
	{
		public override string Name => "Title";

		/// <summary>
		/// CharacterListオブジェクト
		/// </summary>
		[SerializeField]
		private TMP_Dropdown characterList;

		/// <summary>
		/// Titleシーンで「Start」ボタンを押した時に実行される
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
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Wanderer);
						break;
					}
				default:
					{
						parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
						break;
					}
			}

			// PlayGameSceneにパラメータを渡す
			SceneManager.Instance.Load<PlayGameScene>(parameter);
		}
	}
}