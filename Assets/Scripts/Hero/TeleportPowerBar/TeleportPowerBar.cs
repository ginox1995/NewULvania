using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewULVania.Hero.TeleportPowerBar
{
    public class TeleportPowerBar : MonoBehaviour
    {
        public Transform parentPowerBar;
        public Transform powerBar;
        public Transform hero;
        public float teleportDistance;
        SpriteRenderer srHero;
        // Start is called before the first frame update
        void Start()
        {
            srHero = hero.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && powerBar.localScale.x >= parentPowerBar.localScale.x)
            {
                float flip = (srHero.flipX == false) ? 1f : -1f;
                hero.position += new Vector3(teleportDistance * flip, 0f, 0f);
                PowerBarReset();
            }
        }



        public void PowerBarReset()
        {
            powerBar.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}