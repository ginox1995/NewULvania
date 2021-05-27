using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Hero
{
    public class TeleportState : HeroState
    {
        private Animator animator;
        private Transform rb;

        private float teleportDistance;
        private float teleportEnergy;
        private float teleportEnergyRequired;

        public TeleportState(HeroController hero, HeroStateMachine fsm) 
                : base(hero, fsm)
            {
                rb = GameObject.Find("Heros").transform;
                animator = hero.GetComponent<Animator>();
                teleportDistance = hero.teleportDistance;
                teleportEnergy = GameObject.Find("PowerBar").transform.localScale.x;
                teleportEnergyRequired = GameObject.Find("PowerEnergy").transform.localScale.x;
            }

        // Start is called before the first frame update
        public override void OnEnter()
        {
            animator.SetTrigger("teleport");
        }


        public override void OnHandleInput()
        {
            base.OnHandleInput();
            teleportEnergy = Input.GetAxisRaw("Horizontal");
        }

        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
            if (teleportEnergy == teleportEnergyRequired)
            {
                hero.Stop();
            }
        }

        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
            rb = new Vector3(rb.transform.localPosition.x + teleportDistance, rb.transform.localPosition.y, 0f);
        }

    }
}