using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Morion Anima", menuName = "Morion/Perfil de Animacion UI")]
public class CambioTama : ScriptableObject
{
    public bool autoInicializar = true;
    public Rect rectanguloInicial;
    public Rect rectanguloFinal;
    public AnimationCurve curva;
    public float duracion = 1;
    public float offset = 0;

    public bool tieneImagen;
    [ConditionalHide("tieneImagen", true)]
    public Color colorInicial = Color.white;
    [ConditionalHide("tieneImagen", true)]
    public Color colorFinal = Color.white;

}
