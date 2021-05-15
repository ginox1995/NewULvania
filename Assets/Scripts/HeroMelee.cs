using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMelee : MonoBehaviour
{
    private Animator animator;
    private GameObject contactPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        contactPoint = transform.Find("ContactPoint").gameObject;
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
        contactPoint.SetActive(true);
    }
}
