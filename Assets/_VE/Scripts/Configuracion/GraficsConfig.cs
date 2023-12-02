// Importa las bibliotecas necesarias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Si se est� utilizando el Editor de Unity, importa la biblioteca de UnityEditor
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Clase que representa la configuraci�n de gr�ficos como un ScriptableObject.
/// </summary>
[CreateAssetMenu(menuName = "Configuracion/Configuracion de graficos")]
public class GraficsConfig : ScriptableObject
{
    /// <summary>
    /// Tipo de gr�ficos seleccionados (bajos, medios, altos).
    /// </summary>
    public TipoGraficos tipoGraficos;

    /// <summary>
    /// Plataformas seleccionadas mediante flags (Movil, Windows, VR).
    /// </summary>
    public Plataformas plataformas;

    /// <summary>
    /// Obtiene un prefijo basado en el tipo de gr�ficos seleccionado.
    /// </summary>
    /// <returns>El prefijo correspondiente al tipo de gr�ficos.</returns>
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

/// <summary>
/// Enumeraci�n que representa los niveles de gr�ficos (bajos, medios, altos).
/// </summary>
public enum TipoGraficos
{
    bajos = 0,
    medios = 1,
    altos = 2
}

/// <summary>
/// Enumeraci�n de Flags que representa las plataformas disponibles (Movil, Windows, VR).
/// </summary>
[System.Flags]
public enum Plataformas
{
    Nada = 0,
    Movil = 1 << 1,
    Windows = 1 << 2,
    VR = 1 << 3
}

/// <summary>
/// Clase sellada que define un atributo de EnumFlags para ser utilizado en la interfaz del editor.
/// </summary>
public sealed class EnumFlagsAttribute : PropertyAttribute
{
    /// <summary>
    /// Constructor por defecto de EnumFlagsAttribute.
    /// </summary>
    public EnumFlagsAttribute() { }

    /// <summary>
    /// Obtiene los �ndices de las opciones seleccionadas en la enumeraci�n.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeraci�n.</typeparam>
    /// <param name="val">Valor de la enumeraci�n con flags.</param>
    /// <returns>Lista de �ndices de las opciones seleccionadas.</returns>
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

    /// <summary>
    /// Obtiene las cadenas de las opciones seleccionadas en la enumeraci�n.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeraci�n.</typeparam>
    /// <param name="val">Valor de la enumeraci�n con flags.</param>
    /// <returns>Lista de cadenas de las opciones seleccionadas.</returns>
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

// Si se est� utilizando el Editor de Unity, define un PropertyDrawer para el atributo EnumFlags
#if UNITY_EDITOR
/// <summary>
/// Clase que define el dibujante personalizado para el atributo EnumFlags.
/// </summary>
[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// Sobrescribe el m�todo OnGUI para personalizar la interfaz del editor.
    /// </summary>
    /// <param name="position">Rect�ngulo de posici�n del campo en el Editor.</param>
    /// <param name="property">Propiedad serializada asociada al campo.</param>
    /// <param name="label">Etiqueta del campo.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Utiliza EditorGUI.MaskField para mostrar un campo de m�scara de bits en el Editor
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
    }
}
#endif
