using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Animator animator;
    public Transform powerBar;
    public Transform hero;
    public float teleportDistance;
    public SpriteRenderer srHero;
    // Start is called before the first frame update
    void Start()
    {
        srHero = hero.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && powerBar.localScale.x == 1f)
        {
            animator.SetTrigger("teleport");
            float flip = (srHero.flipX == false) ? 1f : -1f;
            hero.position += new Vector3(teleportDistance * flip, 0f, 0f);
            if (teleportCollition()) 
            {
                animator.SetTrigger("die");
            }
            PowerBarReset(); 
        }
    }

    public bool teleportCollition()
    {
        bool collition = false;
        collition = Physics2D.IsTouching(hero.GetComponent<CapsuleCollider2D>(), GameObject.FindGameObjectsWithTag("Wall").GetComponent<TilemapCollider2D>()) &&
           Physics2D.IsTouching(hero.GetComponent<CapsuleCollider2D>(), GameObject.FindGameObjectsWithTag("Fall").GetComponent<TilemapCollider2D>()); 
        return collition;
    }

    public void PowerBarReset()
    {
        powerBar.localScale = new Vector3(0f, 1f, 1f);
        powerBar.localPosition = new Vector3(-0.5f, 0f, 0f);
    }
}
