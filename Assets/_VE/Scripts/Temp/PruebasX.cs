using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasX : MonoBehaviour
{
    public int numero = 5;
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
