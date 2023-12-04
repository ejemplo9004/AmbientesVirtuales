using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasX : MonoBehaviour
{
    public int numero = 5;

    void Start()
    {
        GetArg();
    }
    // Helper function for getting the command line arguments
    private void GetArg()
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-iniciar")
            {
                Cargar();
            }
        }
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            Cargar();
		}
		if (Input.GetMouseButtonDown(0))
		{
            numero--;
			if (numero < 1)
			{
				Cargar();
			}
		}
    }

    public void Cargar()
	{
        Escenas.CargarEscena("CIS");
    }
}
