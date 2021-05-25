using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ULVania.Enemy
{
    public class SpawningState : EnemyState
    {
        private Slider slider;
        public SpawningState(EnemyController enemy, EnemyStateMachine fsm) : base(enemy, fsm)
        {
            slider = enemy.transform.Find("Canvas").Find("Healthbar").GetComponent<Slider>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            slider.maxValue = enemy.Health;
            slider.value = enemy.Health;
            slider.minValue = 0f;
        }
    }
}


