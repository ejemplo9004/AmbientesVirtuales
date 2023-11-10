using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MorionUIAnim))]
public class MorionUIAnimEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		MorionUIAnim cam = (MorionUIAnim)target;
		for (int i = 0; i < cam.animacionesTamaño.Length; i++)
		{
			if (GUILayout.Button("> Iniciar " + i))
			{
				cam.Iniciar(i);
			}
		}
	}
}
