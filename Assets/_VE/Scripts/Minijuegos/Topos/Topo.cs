using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Topo : MonoBehaviour
{
    public bool activo = true;
    private Animator anims;
    public ParticleSystem particulas;
    void Start()
    {
        anims = GetComponent<Animator>();
        StartCoroutine(TopoActivo());
		if (particulas == null)
		{
            particulas = GetComponentInChildren<ParticleSystem>();
		}
    }

    IEnumerator TopoActivo()
    {
		while (true)
		{
            yield return new WaitUntil(() => activo);
            while (activo)
            {
                yield return new WaitForSeconds(Random.Range(3f, 10f));
                anims.SetBool("visible", true);
                yield return new WaitForSeconds(1f);
                anims.SetBool("visible", false);
            }
        }
    }

	private void OnMouseDown()
	{
        Golpear();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Mazo"))
		{
            Golpear();
		}
	}

    void Golpear()
	{
		if (anims.GetBool("visible"))
		{
            if (particulas != null)
            {
                particulas.Play();
            }
        }
        
    }
}
