using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform hero;
    public Transform floor;
    public float heroSpeed;
    public float heroJumpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    bool doubleJump=false;
    float jumpCount = 0;

    public Transform[] spawnEnemyPosition;
    public Transform[] checkSpawnEnemy;
    public Transform[] spawnEnemyPosition2;
    public Transform[] checkSpawnEnemy2;
    public GameObject prefabEnemy;
    public GameObject prefabGhost;


    Rigidbody2D rbHero;
    Animator animmatorHero;
    SpriteRenderer srHero;
    CapsuleCollider2D colliderHero;
    Transform contactPoint;
    //private bool isRunning = false;
    private float movement;

    public bool[] enemyInstantiated;
    public bool[] ghostInstantiated;
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
        for (int i = 0; i < checkSpawnEnemy.Length; i++)
        {
            if (hero.position.x > checkSpawnEnemy[i].position.x && enemyInstantiated[i]==false)
            {
                //Debemos spawnear enemigos
                Instantiate(prefabEnemy, spawnEnemyPosition[i].position,Quaternion.identity);
                enemyInstantiated[i] = true;
            }
        }
        for (int i = 0; i < checkSpawnEnemy2.Length; i++)
        {
            if (hero.position.x > checkSpawnEnemy2[i].position.x && ghostInstantiated[i]==false)
            {
                Instantiate(prefabGhost, spawnEnemyPosition2[i].position, Quaternion.identity);
                ghostInstantiated[i] = true;
            }
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
        }else if (movement > 0)
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
            jumpCount = 0;
            
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (!IsJumping())
            {
                Jump();
                jumpCount += 1;
                doubleJump = true;
                Debug.Log("Primer salto");
            }
            else
            {
                if (doubleJump)
                {
                    Debug.Log("Doble salto");
                    Jump();
                    doubleJump = false;
                    jumpCount += 1;
                }
            }
            
        }
        
        
        if (rbHero.velocity.y < 0)
        {
            // Esta cayendos
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
            colliderHero.bounds.extents.y + 0.3f
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
            Vector2.down * (colliderHero.bounds.extents.y + 0.3f),
            color);
    }
}
