using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace Flappy.UI
{
	// RaycastTargetとして機能させるだけのクラス
	public class CommonModal : Image
	{
		public bool IsDrawing = true;

		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if( this.IsDrawing == true )
			{
				base.OnPopulateMesh(toFill);
				return;
			}

			toFill.Clear();
		}
	}

#if UNITY_EDITOR
	[CanEditMultipleObjects, CustomEditor(typeof(CommonModal), true)]
	public class ModalEditor : ImageEditor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			this.serializedObject.Update();
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("IsDrawing"), true);
			this.serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}