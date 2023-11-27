using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializadorPlataformas : MonoBehaviour
{
    public ElementosDesactivables[] elementosDesactibables;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < elementosDesactibables.Length; i++)
		{
            elementosDesactibables[i].Inicializar();
		}
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
    [ConditionalHide("dependeSerOwner", true)]
    public PlayerControl playerControl;

    public void Inicializar()
	{
        bool activo = GraficsConfig.configuracionDefault.VerificarPlataforma(plataformas);
		if (dependeSerOwner && playerControl != null)
		{
            activo = activo && playerControl.esPropio;
		}
        for (int i = 0; i < objetos.Length; i++)
		{
            objetos[i].SetActive(activo);
        }
	}
}
