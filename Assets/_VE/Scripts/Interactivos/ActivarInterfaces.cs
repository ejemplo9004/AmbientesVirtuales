using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarInterfaces : MonoBehaviour
{

    public GameObject[] interfaces;
    public Text Turno;
    public ParticleSystem particulaAbrir;
    public ParticleSystem particulaTurno;

    public Animator animator;
    private bool activado = false;

    public string NombreAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("El objeto no tiene Animator.");
        }

    }

    void Update()
    {
    
        
    }

    public void Interactuo()
    { 
    

    }

    public void Encender() 
    {
    

    }
    private void OnTriggerEnter(Collider other) 
    {

        if (other.CompareTag("Player"))
        {
            activado = true;
            animator.SetBool(NombreAnim, activado);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            activado = false;
            animator.SetBool(NombreAnim, activado);

        }
    }
}
