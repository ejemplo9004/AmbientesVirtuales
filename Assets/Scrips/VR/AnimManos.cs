using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimManos : MonoBehaviour
{
    public InputActionProperty accionGrip;
    public InputActionProperty accionTrigger;
    public Animator animator;

    void Start()
    {
        accionGrip.action.Enable();
        accionTrigger.action.Enable();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Grip", accionGrip.action.ReadValue<float>());
        animator.SetFloat("Trigger", accionTrigger.action.ReadValue<float>());
    }
}
