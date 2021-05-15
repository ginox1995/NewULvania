using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    Spawn, Walk, Destroy
}

public class EnemyMovement : MonoBehaviour
{
    private EnemyState state;
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 4f;

    private void Start()
    {
        state = EnemyState.Walk;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (state == EnemyState.Walk)
        {
            Walk();
        }
    }

    private void Walk()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ContactPoint")
        {
            Debug.Log("Le pego al enemigo");
        }
        
    }
}
