using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Camera Camera;
    public float Speed;
    public Animator Animator;
    public Harvester Harvester;

    private void FixedUpdate()
    {
        
        Movement();
        Animations();
    }

    private void Movement()
    {
        //Configuracion del movimiento y rotacion de el.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = Camera.transform.TransformDirection(direction);
        direction.y = 0;
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Rigidbody.rotation = lookRotation;

        }

        Vector3 movementV = direction * Speed * Time.fixedDeltaTime;
        Rigidbody.linearVelocity = movementV;
    }
    private void Animations()
    {
        if (IsMove())
        {
            if (Harvester.HasProducts())
            {
                Animator.Play("CharacterArmature|Run_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Run");
            }
        }
        else
        {

            if (Harvester.HasProducts())
            {
                Animator.Play("CharacterArmature_Idle_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Idle");
            }
        }
    }

    //Nos dice si nos estamos movimiento o no, para activar la animacion
    private bool IsMove()
    {
        return Rigidbody.linearVelocity != Vector3.zero;
    }
}
