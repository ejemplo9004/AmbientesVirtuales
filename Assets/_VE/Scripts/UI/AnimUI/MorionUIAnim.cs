using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MorionUIAnim : MonoBehaviour
{
    public RectTransform rt;
    public CambioTamaInterno[] animacionesTama�o;

	private void Awake()
	{
		for (int i = 0; i < animacionesTama�o.Length; i++)
		{
            animacionesTama�o[i].Inicializar();
		}
	}
	void Start()
    {
		if (rt == null)
		{
            rt = GetComponent<RectTransform>();
		}
    }
    public void Iniciar(int cual)
    {
		if (cual < 0 || cual >= animacionesTama�o.Length)
		{
            Debug.LogError("Est� intentando reproducir una animaci�n fuera del rango: " + cual);
            return;
		}
		if (rt == null)
        {
            Debug.LogError("No hay un Rect Transform Objetivo para la animaci�n.");
            return;
        }
        animacionesTama�o[cual].Iniciar(rt, this);
    }
}
[System.Serializable]
public class CambioTamaInterno
{
    RectTransform rt;
    public CambioTama perfil;
    public UnityEvent eventoAlIniciar;
    public Rect rectanguloInicial;
    public Rect rectanguloFinal;
    public AnimationCurve curva;
    public float duracion = 1;
    public float offset = 0;

    public Image imagen;
    public Color colorInicial = Color.white;
    public Color colorFinal = Color.white;

    public UnityEvent eventoAlFinalizar;

    public void Inicializar()
	{
		if (perfil != null)
		{
            rectanguloInicial = perfil.rectanguloInicial;
            rectanguloFinal = perfil.rectanguloFinal;
            curva = perfil.curva;
            duracion = perfil.duracion;
            offset = perfil.offset;
            colorInicial = perfil.colorInicial;
            colorFinal = perfil.colorFinal;
		}
	}
    public void Iniciar(RectTransform r, MonoBehaviour m)
    {
        rt = r;
        m.StartCoroutine(Restaurar());
    }
    IEnumerator Restaurar()
    {
        eventoAlIniciar.Invoke();
        yield return new WaitForSeconds(offset);
        Vector3 bPos = new Vector3(rectanguloInicial.position.x, rectanguloInicial.position.y, 0);
        Vector3 nPos = new Vector3(rectanguloFinal.position.x, rectanguloFinal.position.y, 0);
        for (int i = 0; i <= 20; i++)
        {
            float t = curva.Evaluate(i / 20f);
            yield return new WaitForSeconds(duracion / 20f);
            rt.sizeDelta = Vector2.LerpUnclamped(rectanguloInicial.size, rectanguloFinal.size, t);
            rt.localPosition = Vector3.LerpUnclamped(bPos, nPos, t);
			if (imagen != null)
			{
                imagen.color = Color.Lerp(colorInicial, colorFinal, t);
			}
        }
        eventoAlFinalizar.Invoke();
    }
}