using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Hero
{
    public class TeleportState : HeroState
    {
        private Animator animator;
        private Transform rbHero;
        private Transform rbPowerBar;
        private Transform rbPowerEnergy;
        private SpriteRenderer srHero;


        private float teleportDistance;
        private float teleportEnergy;
        private float teleportEnergyRequired;

        public TeleportState(HeroController hero, HeroStateMachine fsm) 
                : base(hero, fsm)
            {
                Debug.Log("s");
                rbHero = GameObject.Find("Heros").transform;
                srHero = hero.GetComponent<SpriteRenderer>();
                animator = hero.GetComponent<Animator>();
                teleportDistance = hero.teleportDistance;
                rbPowerBar = GameObject.Find("PowerBar").transform;
                rbPowerEnergy = GameObject.Find("PowerEnergy").transform;
                teleportEnergy = GameObject.Find("PowerBar").transform.localScale.x;
                teleportEnergyRequired = GameObject.Find("PowerEnergy").transform.localScale.x;
            }

        // Start is called before the first frame update
        public override void OnEnter()
        {
            if (teleportEnergy != teleportEnergyRequired)
            {
                hero.Stop();
            }
            else 
            { 
                animator.SetTrigger("teleport"); 
            }
        }

        /*public virtual void OnExit() 
        { 
            //teleportEnergy
        }
        */
        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
            float flip = (srHero.flipX == false) ? 1f : -1f;
            rbHero.position += new Vector3(teleportDistance * flip, 0f, 0f);
        }

    }
}