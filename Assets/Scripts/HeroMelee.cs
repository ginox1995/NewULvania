using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULVania.Enemy;
using NewULVania.Hero.TeleportPowerBar;

public class HeroMelee : MonoBehaviour
{
    private Animator animator;
    private GameObject contactPoint;

    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private int damage = 2;

    public float rate = 1f;
    private float counter = 0f;
    public Transform parentPowerBar;
    public Transform powerBar;
    public float powerBarGrowRate;

    private void Start()
    {
        animator = GetComponent<Animator>();
        contactPoint = transform.Find("ContactPoint").gameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > counter)
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
                PowerBarGrow();
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

    private void PowerBarGrow()
    {
        if (powerBar.localScale.x <= parentPowerBar.localScale.x)
            powerBar.localScale += new Vector3(parentPowerBar.localScale.x * powerBarGrowRate, parentPowerBar.localScale.y * powerBarGrowRate, 0f);

    }

    private void PowerBarReset()
    {
        powerBar.localScale = new Vector3(0f, 0f, 0f);
    }
}
