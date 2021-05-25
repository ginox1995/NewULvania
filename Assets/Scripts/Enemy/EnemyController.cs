using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public float speed;
        [SerializeField]
        public int Health;

        private EnemyStateMachine fsm;

        //States
        private EnemyState spawningState;
        private EnemyState walkingState;
        private EnemyState dyingState;
        private void Start()
        {
            fsm = new EnemyStateMachine();

            spawningState = new SpawningState(this, fsm);
            walkingState = new WalkingState(this, fsm);
            dyingState = new DyingState(this, fsm);

            //Seteo estado inicial
            fsm.Start(spawningState);
            fsm.ChangeState(walkingState);
        }

        private void FixedUpdate()
        {
            fsm.GetCurrentState().OnPhysicsUpdate();
        }

        private void Update()
        {
            fsm.GetCurrentState().OnHandleInput();
            fsm.GetCurrentState().OnLogicUpdate();
        }

        public void Hurt(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                fsm.ChangeState(dyingState);
            }
        }
    }
}

