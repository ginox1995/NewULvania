using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Enemy
{
    public class WalkingState : EnemyState
    {
        private Rigidbody2D rb;
        private float speed;
        public WalkingState(EnemyController enemy, EnemyStateMachine fsm) : base(enemy, fsm)
        {
            rb = enemy.GetComponent<Rigidbody2D>();
            speed = enemy.speed;
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

}
