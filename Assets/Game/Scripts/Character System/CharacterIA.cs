using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterIA : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform TargetT;
    public Animator Animator;
    public Shopper Shopper;


    private void FixedUpdate()
    {
        Animations();
    }
    private void Animations()
    {
        if (IsMove())
        {
            if (Shopper.HasProductsIA())
            {
                Animator.Play("CharacterArmature|Walk_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Walk");
            }

        }
        else
        {
            if (Shopper.HasProductsIA())
            {
                Animator.Play("CharacterArmature_Idle_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Idle");
            }
        }
    }

    private bool IsMove()
    {
        return Agent.velocity.magnitude > 0;
    }

    private void Move(Vector3 destinarionPos)
    {
        Agent.SetDestination(destinarionPos);
    }
    public void MoveToTransform(Transform destinarionT)
    {
        TargetT.position = destinarionT.position;
        MoveTo();

    }
    private void MoveTo()
    {
        Move(TargetT.position);
    }

    internal bool Contains()
    {
        throw new NotImplementedException();
    }

    internal bool Contains(CharacterIA characterIA)
    {
        throw new NotImplementedException();
    }
}


