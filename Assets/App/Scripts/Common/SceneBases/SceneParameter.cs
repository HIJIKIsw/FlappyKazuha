using System;
using System.Collections.Generic;

namespace Flappy.Common
{
	/// <summary>
	/// シーン間でパラメータを受け渡すためのクラス
	/// </summary>
	/// <remarks>各パラメータは受け取る側のシーンクラスにenumを実装して、それをキーとして渡す</remarks>
	public class SceneParameter : Dictionary<Enum, object>
	{
		public T Get<T>(Enum key)
		{
			if (TryGetValue(key, out var value) && value is T typedValue)
			{
				return typedValue;
			}
			else
			{
				throw new ArgumentException($"Key '{key}' not found or value cannot be cast to type '{typeof(T)}'.");
			}
		}
	}
}