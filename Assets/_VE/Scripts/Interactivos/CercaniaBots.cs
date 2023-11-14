using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercaniaBots : MonoBehaviour
{
    public Animator animator;
    private bool activado = false;

    // Start is called before the first frame update

    void Start()
    {
        animator = this.GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("El objeto no tiene Animator.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if (other.CompareTag("Player"))
        {
            activado = true;
            animator.SetBool("TabletaOn", activado);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            activado = false;
            animator.SetBool("TabletaOn", activado);

        }
    }
}


  

  

