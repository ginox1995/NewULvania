using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private Animator animator;
    public Transform powerEnergy;
    public Transform powerBar;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true) 
        {
            PowerEnergyGrow();
        }
    }


    private void PowerEnergyGrow()
    {
        if (powerEnergy.localScale.x > damage)
        {
            PowerEnergyBarGrow(damage);
        }
        else
        {
            PowerEnergyBarGrow(powerEnergy.localScale.x);
        }
    }
    private void PowerEnergyBarGrow(float growRate)
    {
        powerEnergy.localScale -= new Vector3(growRate, 0f, 0f);
        powerEnergy.localPosition -= new Vector3(growRate / 2, 0f, 0f);
    }
}
