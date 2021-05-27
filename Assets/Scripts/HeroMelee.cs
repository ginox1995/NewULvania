using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULVania.Enemy;

public class HeroMelee : MonoBehaviour
{
    private Animator animator;
    private GameObject contactPoint;
    public Transform powerEnergy;
    public Transform powerBar;
    public float powerBarGrowRate;

    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private int damage = 2;

    private float rate = 0.5f;
    private float counter = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        contactPoint = transform.Find("ContactPoint").gameObject;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && Time.time>counter)
        {
            // Tenemos iniciar ataque
            Attack();
            counter = Time.time + rate;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            contactPoint.transform.position, attackRange);
        foreach(Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyController>().Hurt(damage);
                PowerEnergyGrow();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (contactPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(contactPoint.transform.position, attackRange);
    }

    private void PowerEnergyGrow()
    {
        if (1f - powerEnergy.localScale.x > powerBarGrowRate)
        {
            PowerEnergyBarGrow(powerBarGrowRate);
        }
        else
        {
            PowerEnergyBarGrow(1f - powerEnergy.localScale.x);
        }
    }
    private void PowerEnergyBarGrow(float growRate)
    {
        powerEnergy.localScale += new Vector3(growRate, 0f, 0f);
        powerEnergy.localPosition += new Vector3(growRate/2, 0f, 0f);
    }
}
