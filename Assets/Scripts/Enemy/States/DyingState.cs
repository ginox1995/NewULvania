using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Enemy
{
    public class DyingState : EnemyState
    {
        public DyingState(EnemyController enemy, EnemyStateMachine fsm) : base(enemy, fsm)
        {
        }
    }

}
