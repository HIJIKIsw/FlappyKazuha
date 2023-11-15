using Flappy.Common;
using Flappy.Manager;
using TMPro;
using UnityEngine;

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
                        Debug.Log (parameter);
                        parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Wanderer);
                        break;
                    }
                default:
                    {
                        Debug.Log (parameter);
                        parameter.Add(PlayGameScene.Parameters.Character, Constants.Assets.Prefab.Player.Kazuha);
                        break;
                    }
            }

            // PlayGameSceneにパラメータを渡す
            SceneManager.Instance.Load<PlayGameScene>(parameter);
        }

    }


}