using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public float speed;

        private EnemyStateMachine fsm;

        // States
        private EnemyState spawningState;
        private EnemyState walkingState;
        private EnemyState dyingState;

        void Start()
        {
            fsm = new EnemyStateMachine();

            spawningState = new SpawningState(this, fsm);
            walkingState = new WalkingState(this, fsm);
            dyingState = new DyingState(this, fsm);

            // Seteo estado inicial
            fsm.Start(spawningState);
            fsm.ChangeState(walkingState);
        }

        void FixedUpdate()
        {
            fsm.GetCurrentState().OnPhysicsUpdate();
        }

        void Update()
        {
            fsm.GetCurrentState().OnHandleInput();
            fsm.GetCurrentState().OnLogicUpdate();
        }
    }

}

