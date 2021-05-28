using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ULVania.Hero;

namespace ULVania.Enemy
{
    public class WalkingState : EnemyState
    {
        private Rigidbody2D rb;
        private CapsuleCollider2D cc;
        private CapsuleCollider2D heroc;
        private float speed;
        private Slider slider;

        float nextDamaged = 0f;
        float damagedRate = 2f;

        public WalkingState(EnemyController enemy, EnemyStateMachine fsm) : base(enemy, fsm)
        {
            rb = enemy.GetComponent<Rigidbody2D>();
            cc = enemy.GetComponent<CapsuleCollider2D>();
            heroc = GameObject.Find("Heros").GetComponent<CapsuleCollider2D>();
            speed = enemy.speed;
            slider = enemy.transform.Find("Canvas").Find("Healthbar").GetComponent<Slider>();
        }
        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
            slider.value = enemy.Health;
        }
        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (Physics2D.IsTouching(cc, heroc))
            {
                if (Time.time > nextDamaged)
                {
                    nextDamaged += damagedRate;
                    Debug.Log("toque");
                    GameObject.Find("HealtBar").GetComponent<EnergyManager>().PowerEnergyGrow(0f);
                }
            }
        } 
    }
}

