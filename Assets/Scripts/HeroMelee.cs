using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMelee : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Tenemos iniciar ataque
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }
}
