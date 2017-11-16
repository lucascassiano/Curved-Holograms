using UnityEngine;
using System.Collections;
// Runtime code here
#if UNITY_EDITOR
// Editor specific code here
using UnityEditor;
#endif
// Runtime code here
/*
[CustomEditor(typeof(CurvedProjection))]
public class CurvedProjectionEditor : Editor
{
	#if UNITY_EDITOR
	// Editor specific code here
	public override void OnInspectorGUI()
	{
		CurvedProjection myScript = (CurvedProjection)target;
		GUILayout.Label("Designed for Curved Projection Mapping\nAuthor: Lucas Cassiano\nVersion: "+ myScript.getVersion());
		//myScript.intensity = GUILayout.HorizontalSlider(myScript.intensity, 0f, 1f);
		//GUILayout.BeginArea(new Rect(10, 10, 100, 100));
		//GUILayout.Button("Click me");
		//GUILayout.Button("Or me");
		//GUILayout.EndArea();
		DrawDefaultInspector();

	}
	#endif
	// Runtime code here

}*/