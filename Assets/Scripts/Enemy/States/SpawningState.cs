using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Enemy
{
    public class SpawningState : EnemyState
    {
        public SpawningState(EnemyController enemy, EnemyStateMachine fsm) : base(enemy, fsm)
        {
        }
    }

}
