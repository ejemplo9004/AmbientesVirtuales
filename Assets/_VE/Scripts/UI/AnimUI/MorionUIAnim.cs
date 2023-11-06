using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorionUIAnim : MonoBehaviour
{
    public RectTransform rt;
    public Rect rectangulo;

    public Rect rectangulo_Inicial;

    void Start()
    {
        StartCoroutine(Restaurar());
    }

    public IEnumerator Restaurar()
	{
        //rectangulo_Inicial = rt.rect;
        yield return new WaitForSeconds(1f);
        //rectangulo= rt.rect;

        for (int i = 0; i <= 20; i++)
		{
            yield return new WaitForSeconds(1 / 20f);
            rt.sizeDelta = Vector2.Lerp(rectangulo.size, rectangulo_Inicial.size, 1f/i);
        }
    }
    void Update()
    {
        
    }
}
