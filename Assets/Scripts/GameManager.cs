using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform hero;
    public float heroSpeed;
    public float heroJumpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public Transform spawnEnemyPosition;
    public Transform checkSpawnEnemy;
    public GameObject prefabEnemy;

    Rigidbody2D rbHero;
    Animator animmatorHero;
    SpriteRenderer srHero;
    CapsuleCollider2D colliderHero;
    Transform contactPoint;
    //private bool isRunning = false;
    private float movement;

    private bool enemyInstantiated = false;

    void Start()
    {
        rbHero = hero.GetComponent<Rigidbody2D>();
        animmatorHero = hero.GetComponent<Animator>();
        srHero = hero.GetComponent<SpriteRenderer>();
        colliderHero = hero.GetComponent<CapsuleCollider2D>();
        contactPoint = hero.transform.Find("ContactPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.transform.position.x > checkSpawnEnemy.position.x && !enemyInstantiated)
        {
            // Debemos spawnear un enemigo
            Instantiate(prefabEnemy, spawnEnemyPosition.position, Quaternion.identity);
            enemyInstantiated = true;
        }

        movement = Input.GetAxisRaw("Horizontal");
        if (movement < 0)
        {
            srHero.flipX = true;
            animmatorHero.SetBool("isRunning", true);
            if (contactPoint.localPosition.x > 0f)
            {
                contactPoint.localPosition = new Vector3(
                    contactPoint.localPosition.x * -1f,
                    contactPoint.localPosition.y,
                    contactPoint.localPosition.z
                );
            }
            
        }
        else if (movement > 0)
        {
            srHero.flipX = false;
            animmatorHero.SetBool("isRunning", true);
            if (contactPoint.localPosition.x < 0f)
            {
                contactPoint.localPosition = new Vector3(
                    contactPoint.localPosition.x * -1f,
                    contactPoint.localPosition.y,
                    contactPoint.localPosition.z
                );
            }
        }
        else
        {
            animmatorHero.SetBool("isRunning", false);
        }

        rbHero.velocity = new Vector2(movement * heroSpeed, rbHero.velocity.y);

        if (!IsJumping()) // puede ser un or (if (!isJumping() && !isJumping <--- no esta tan bien
        {
            animmatorHero.SetBool("isJumping", false);
        }

        if (!IsJumping() && Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        
        if (rbHero.velocity.y < 0)
        {
            // Esta cayendo
            rbHero.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }else if (rbHero.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rbHero.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1  ) * Time.deltaTime;
        }
    }

    private void Jump()
    {
        animmatorHero.SetBool("isJumping", true);
        animmatorHero.SetTrigger("jump");
        rbHero.velocity = new Vector2(rbHero.velocity.x, heroJumpSpeed);
    }

    private bool IsJumping()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            colliderHero.bounds.center,
            Vector2.down,
            colliderHero.bounds.extents.y + 0.2f
        );

        DebugJumpRay(hit);

        return !hit;
    }

    private void DebugJumpRay(RaycastHit2D hit)
    {
        Color color;
        if (hit)
        {
            color = Color.red;
        }
        else
        {
            color = Color.white;
        }

        Debug.DrawRay(colliderHero.bounds.center,
            Vector2.down * (colliderHero.bounds.extents.y + 0.2f),
            color);
    }
}
