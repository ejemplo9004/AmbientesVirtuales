using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
    public static GraficsConfig configuracionGraficos 
    {
		get
		{
            return Resources.Load<GraficsConfig>("ConfiguracionGraficosGeneral");
		}

		set
		{
            configuracionGraficos = value;
        }
    }

    public static void CargarEscena(string nombre)
	{
		SceneManager.LoadScene(configuracionGraficos.GetPrefijo() + "_" + nombre);
		SceneManager.LoadScene("C_" + nombre, LoadSceneMode.Additive);
	}

}
