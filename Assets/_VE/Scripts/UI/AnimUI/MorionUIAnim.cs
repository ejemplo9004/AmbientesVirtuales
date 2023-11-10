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
		if (rt == null)
		{
            rt = GetComponent<RectTransform>();
		}
		for (int i = 0; i < animacionesTama�o.Length; i++)
		{
            animacionesTama�o[i].Inicializar(rt);
		}
	}

    public void GuardarInicial(int cual)
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
        animacionesTama�o[cual].rectanguloInicial.size = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
        animacionesTama�o[cual].rectanguloInicial.position = new Vector2(rt.localPosition.x, rt.localPosition.y);
        if (animacionesTama�o[cual].imagen != null && animacionesTama�o[cual].tieneImagen)
        {
            animacionesTama�o[cual].colorInicial = animacionesTama�o[cual].imagen.color;
        }
    }
    public void GuardarFinal(int cual)
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
        animacionesTama�o[cual].rectanguloFinal.size = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
        animacionesTama�o[cual].rectanguloFinal.position = new Vector2(rt.localPosition.x, rt.localPosition.y);
		if (animacionesTama�o[cual].imagen != null && animacionesTama�o[cual].tieneImagen)
		{
            animacionesTama�o[cual].colorFinal = animacionesTama�o[cual].imagen.color;
        }
    }
    void Start()
    {

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
        animacionesTama�o[cual].Iniciar(this);
    }
}
[System.Serializable]
public class CambioTamaInterno
{
    RectTransform rt;
    public bool autoInicializar;
    public CambioTama perfil;
    public UnityEvent eventoAlIniciar;
    public Rect rectanguloInicial;
    public Rect rectanguloFinal;
    public AnimationCurve curva;
    public float duracion = 1;
    public float offset = 0;

    public bool tieneImagen;
    [ConditionalHide("tieneImagen", true)]
    public Image imagen;
    [ConditionalHide("tieneImagen", true)]
    public Color colorInicial = Color.white;
    [ConditionalHide("tieneImagen", true)]
    public Color colorFinal = Color.white;

    public UnityEvent eventoAlFinalizar;

    public void Inicializar(RectTransform r)
	{
        rt = r;
		if (perfil != null)
        {
            autoInicializar = perfil.autoInicializar;
            rectanguloInicial = perfil.rectanguloInicial;
            rectanguloFinal = perfil.rectanguloFinal;
            curva = perfil.curva;
            duracion = perfil.duracion;
            offset = perfil.offset;
            colorInicial = perfil.colorInicial;
            colorFinal = perfil.colorFinal;
		}
		if (autoInicializar)
        {
            Reiniciar();
        }
	}

    public void Reiniciar()
	{
        Vector3 bPos = new Vector3(rectanguloInicial.position.x, rectanguloInicial.position.y, 0);
        Vector3 nPos = new Vector3(rectanguloFinal.position.x, rectanguloFinal.position.y, 0);
        rt.sizeDelta = Vector2.LerpUnclamped(rectanguloInicial.size, rectanguloFinal.size, 0);
        rt.localPosition = Vector3.LerpUnclamped(bPos, nPos, 0);
        if (imagen != null && tieneImagen) 
            imagen.color = Color.Lerp(colorInicial, colorFinal, 0);
    }
    public void Iniciar(MonoBehaviour m)
    {
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
			if (imagen != null && tieneImagen)
			{
                imagen.color = Color.Lerp(colorInicial, colorFinal, t);
			}
        }
        eventoAlFinalizar.Invoke();
    }
}