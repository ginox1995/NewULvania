using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULVania.Hero
{
    public class EnergyManager : MonoBehaviour
    {
        private Animator animator;
        public CapsuleCollider2D colliderHero;
        public Transform powerEnergy;
        public Transform powerBar;
        public SpriteRenderer healEnergy;
        public float damage;
        float nextDamaged;
        public float damagedRate;

        // Start is called before the first frame update
        void Start()
        {
            nextDamaged = 0f;
            animator = GameObject.Find("Heros").GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void PowerEnergyGrow(float extraDamage)
        {
            damage = damage + extraDamage;
            if (Time.time > nextDamaged)
            {
                nextDamaged += damagedRate;
                animator.SetTrigger("damaged");
                Debug.Log("energy " + powerEnergy.localScale.x);
                Debug.Log("damage " + damage);
                if (powerEnergy.localScale.x > damage)
                {
                    
                    PowerEnergyBarGrow(damage);
                } 
                else
                {
                    PowerEnergyBarGrow(powerEnergy.localScale.x);
                }
            }
            if (extraDamage != 0f) animator.SetTrigger("die"); 
        }

        private void PowerEnergyBarGrow(float growRate)
        {
            powerEnergy.localScale -= new Vector3(growRate, 0f, 0f);
            powerEnergy.localPosition -= new Vector3(growRate/2, 0f, 0f);
            EnergyBarColorChange();
            
        }

        private void EnergyBarColorChange()
        {
            if (powerEnergy.localScale.x == 0f )
            {
                animator.SetTrigger("die");
            }
            else if (powerEnergy.localScale.x <  0.25f)
            {
                healEnergy.color = Color.red;
            }
            else if (powerEnergy.localScale.x < 0.7f)
            {
                healEnergy.color = Color.yellow;
            }
            else if (powerEnergy.localScale.x >= 0.7f)
            {
                
                healEnergy.color = Color.green;
            }
        }
    }
}