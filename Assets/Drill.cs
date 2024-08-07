using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float damagePerSecond = 20;

    private bool isSpinning = false;
    private AudioSource drillSound;
    [SerializeField] private Vehicle vehicle;
    [SerializeField] private float fuelConsumptionPerSecond = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 0;
        animator.Play("Drill");
        drillSound = GetComponent<AudioSource>();
        StartCoroutine(DrillSpinRoutine());
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!isSpinning) {
            return;
        }

        if (collider.gameObject.CompareTag("Mineable")) {
            Mineral mineable = collider.gameObject.GetComponent<Mineral>();
            mineable.ApplyDamage(damagePerSecond * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicle.GetFuelPercentage() == 0 || (vehicle.CompareTag("Player") && UIManager.GetInstance().IsUIOpen()))
        {
            PowerDown();
        }
    }

    private IEnumerator DrillSpinRoutine()
    {
        while (true)
        {
            if (isSpinning)
            {
                vehicle.BurnFuel(fuelConsumptionPerSecond * Time.deltaTime);
            }
            
            yield return null;
        }
    }

    public bool IsSpinning()
    {
        return isSpinning;
    }

    public void SpinUp()
    {
        if (vehicle.GetFuelPercentage() == 0 || vehicle.IsDestroyed())
        {
            return;
        }

        if (!isSpinning) {
            drillSound.Play();
        }
        isSpinning = true;
        animator.speed = 1;
    }

    public void PowerDown()
    {
        if (isSpinning) {
            drillSound.Stop();
        }
        isSpinning = false;
        animator.speed = 0;
    }
}
