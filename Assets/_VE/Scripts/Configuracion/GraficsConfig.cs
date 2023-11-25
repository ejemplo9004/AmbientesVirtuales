using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(menuName ="Configuracion/Configuracion de graficos")]

public class GraficsConfig : ScriptableObject
{
    public TipoGraficos tipoGraficos;
    public Plataformas  plataformas;

    public string GetPrefijo()
	{
		switch (tipoGraficos)
		{
			case TipoGraficos.bajos:
				return "LP";
			case TipoGraficos.medios:
				return "MP";
			case TipoGraficos.altos:
				return "HP";
		}
		return "";
	}
}

public enum TipoGraficos
{
	bajos = 0,
	medios = 1,
	altos = 2
}

[System.Flags]
public enum Plataformas
{
    Nada = 0,
    Movil = 1 << 1,
    Windows = 1 << 2,
    VR = 1 << 3
}

public sealed class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }

    public static List<int> GetSelectedIndexes<T>(T val) where T : IConvertible
    {
        List<int> selectedIndexes = new List<int>();
        for (int i = 0; i < System.Enum.GetValues(typeof(T)).Length; i++)
        {
            int layer = 1 << i;
            if ((Convert.ToInt32(val) & layer) != 0)
            {
                selectedIndexes.Add(i);
            }
        }
        return selectedIndexes;
    }
    public static List<string> GetSelectedStrings<T>(T val) where T : IConvertible
    {
        List<string> selectedStrings = new List<string>();
        for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
        {
            int layer = 1 << i;
            if ((Convert.ToInt32(val) & layer) != 0)
            {
                selectedStrings.Add(Enum.GetValues(typeof(T)).GetValue(i).ToString());
            }
        }
        return selectedStrings;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
    }
}
#endif