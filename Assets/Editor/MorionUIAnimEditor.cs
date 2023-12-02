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
			var style = new GUIStyle(GUI.skin.button);
			style.normal.textColor = Color.green;
			if (GUILayout.Button("> Iniciar " + i, style))
			{
				cam.Iniciar(i);
			}
			if (GUILayout.Button("Guardar Rectangulo Inicial de " + i))
			{
				cam.GuardarInicial(i);
			}
			if (GUILayout.Button("Guardar Rectangulo Final de " + i))
			{
				cam.GuardarFinal(i);
			}
			if (cam.animacionesTamaño[i].usarPerfil && cam.animacionesTamaño[i].perfil != null && GUILayout.Button("Cargar Perfil - " + i))
			{
				cam.animacionesTamaño[i].CargarPerfil();
			}
		}
	}
}
