using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerControl))]

public class InicializadorPlataformas : MonoBehaviour
{
    public ElementosDesactivables[] elementosDesactibables;
    PlayerControl playerControl;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        playerControl = GetComponent<PlayerControl>();
		if (playerControl != null)
		{
            playerControl.posConfigurar += IniciarTodo;
		}
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        IniciarTodo();
    }

    public void IniciarTodo()
	{
        if (playerControl.esPropio)
        {
            playerControl.plataforma.Value = (int)GraficsConfig.configuracionDefault.plataformaObjetivo;
        }
        for (int i = 0; i < elementosDesactibables.Length; i++)
        {
            elementosDesactibables[i].Inicializar(playerControl);
        }
        print("Inicializado " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

[System.Serializable]
public class ElementosDesactivables
{
    public GameObject[] objetos;
    public Plataformas plataformas;
    public bool dependeSerOwner;

    public void Inicializar(PlayerControl playerControl)
	{
		if (playerControl != null)
		{
			if (playerControl.esPropio)
			{
                bool activo = GraficsConfig.configuracionDefault.VerificarPlataforma(plataformas);
                if (dependeSerOwner)
		        {
                    activo = activo && playerControl.esPropio;
		        }
                for (int i = 0; i < objetos.Length; i++)
		        {
                    objetos[i].SetActive(activo);
                }
			}
			else
			{
                bool activo = GraficsConfig.configuracionDefault.VerificarPlataforma(plataformas, playerControl.plataforma.Value);
                if (dependeSerOwner)
                {
                    activo = activo && playerControl.esPropio;
                }
                for (int i = 0; i < objetos.Length; i++)
                {
                    objetos[i].SetActive(activo);
                }
            }
		}

	}
}
