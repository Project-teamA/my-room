using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 波情報インスペクタの拡張機能
/// </summary>
[CustomEditor(typeof(WavesInfo))]
public class WavesInfoEditor : Editor
{
	/// <summary>
	/// Raises the inspector GU event.
	/// </summary>
	public override void OnInspectorGUI()
	{
		// Show default inspector property editor
		DrawDefaultInspector();
		EditorGUILayout.HelpBox("Waves number automaticaly calculated from Spawn Points", MessageType.Info);
	}
}
