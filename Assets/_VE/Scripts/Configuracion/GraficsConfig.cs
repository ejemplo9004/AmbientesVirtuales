using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Configuracion/Configuracion de graficos")]
public class GraficsConfig : ScriptableObject
{
    public TipoGraficos tipoGraficos;

    public string GetPrefijo()
	{
		switch (tipoGraficos)
		{
			case TipoGraficos.bajos:
				return "LP";
			case TipoGraficos.medios:
				return "MP";
			case TipoGraficos.Altos:
				return "HP";
		}
		return "";
	}
}

public enum TipoGraficos
{
    bajos = 0,
    medios = 1,
    Altos = 2
}
