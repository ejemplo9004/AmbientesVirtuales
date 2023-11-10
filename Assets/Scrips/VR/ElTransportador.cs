using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElTransportador : MonoBehaviour
{
    public static ElTransportador singleton;
    public Image imFondo;
    public Transform[] lugares;

    float t;
    bool bloqueo = false;

	private void Awake()
	{
        singleton = this;
	}

    public void Teletransportar(int cual)
	{
		if (bloqueo)
		{
			return;
		}
        StartCoroutine(Teleportar(cual));
	}

    IEnumerator Teleportar(int cual)
	{
		bloqueo = true;
		Color negro = Color.black;
		for (int i = 0; i <= 20; i++)
		{
			t = i / 20f;
			negro.a = t;
			imFondo.color = negro;
			yield return new WaitForSeconds(0.02f);
		}
		transform.position = lugares[cual].position;
		transform.rotation = lugares[cual].rotation;
		for (int i = 0; i <= 20; i++)
		{
			t = i / 20f;
			negro.a = 1-t;
			imFondo.color = negro;
			yield return new WaitForSeconds(0.02f);
		}
		bloqueo = false;
	}

}
