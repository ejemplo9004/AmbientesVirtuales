using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasX : MonoBehaviour
{
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            Escenas.CargarEscena("CIS");
		}
    }
}
