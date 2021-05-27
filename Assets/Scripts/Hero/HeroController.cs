using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Hero
{
    public class HeroController : MonoBehaviour
    {
        public float speed = 2f;
        public float teleportDistance = 5f;
        public float teleportEnergy = 0f;
        public float teleportEnergyRequired = 1f;
        private HeroStateMachine fsm;

        //States
        private HeroState teleportState;
        private HeroState idleState;
        private HeroState runningState;
        private void Start()
        {
            fsm = new HeroStateMachine();

            idleState = new IdleState(this, fsm);
            runningState = new RunningState(this, fsm);
            teleportState = new TeleportState(this, fsm);

            fsm.Start(idleState);
        }

        public void Move()
        {
            fsm.ChangeState(runningState);
        }
        public void Stop()
        {
            fsm.ChangeState(idleState);
        }
        public void Teleport()
        {
            fsm.ChangeState(teleportState);
        }
    }
}

