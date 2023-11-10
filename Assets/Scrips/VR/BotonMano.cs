using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BotonMano : MonoBehaviour
{
    public AnimationCurve curvaIn;
    public AnimationCurve curvaOut;
    public InputActionProperty activador;
    public float esperas = 0.05f;
    public float desface = 0.1f;
    public UnityEvent accion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animacion());
    }

    public IEnumerator Animacion()
	{
		while (true)
		{
            transform.localScale = Vector3.zero;
            yield return new WaitUntil(() => activador.action.ReadValue<float>() > 0.8f);
            yield return new WaitForSeconds(desface);
            for (int i = 0; i <= 10; i++)
			{
                transform.localScale = Vector3.one * curvaIn.Evaluate(i / 10f);
                yield return new WaitForSeconds(esperas);
            }
            yield return new WaitUntil(() => activador.action.ReadValue<float>() < 0.2f);
            for (int i = 0; i <= 10; i++)
            {
                transform.localScale = Vector3.one * curvaOut.Evaluate(i / 10f);
                yield return new WaitForSeconds(esperas);
            }
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Dedo"))
		{
            accion.Invoke();   
		}
	}
}
