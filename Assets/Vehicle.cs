using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private Rigidbody2D rb;
    //private AudioSource aud;
    private List<VehiclePart> parts;

    private ContactFilter2D terrainFilter = new();

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Tool heldTool = null;
    //[SerializeField] private float hullPoints = 100.0f;
    [SerializeField] private float maxHullPoints = 100.0f;
    //[SerializeField] private float fuelPoints = 100.0f;
    [SerializeField] private float maxFuelPoints = 100.0f;
    [SerializeField] private float movementFuelDivisor = 1000f;
    [SerializeField] private float jumpFuelCost = 1.0f;
    //private bool destroyed = false;
    private bool onFire = false;
    [SerializeField] private SpriteRenderer flameOverlay;
    [SerializeField] private float fireDamagePerSecond = 10.0f;
    private CharacterData data;

    // Start is called before the first frame update
    void Start()
    {
        data = new CharacterData
        {
            Destroyed = false,
            FuelPoints = maxFuelPoints,
            HullPoints = maxHullPoints
        };

        rb = GetComponent<Rigidbody2D>();
        //aud = GetComponent<AudioSource>();
        parts = new List<VehiclePart>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out VehiclePart part)) {
                parts.Add(part);
            }
        }

        terrainFilter.layerMask = LayerMask.NameToLayer("Terrain");
        StartCoroutine(FireDamageRoutine());
    }

    public CharacterData GetCharacterData()
    {
        return data;
    }

    public void SetCharacterData(CharacterData data)
    {
        this.data = data;
    }

    public void LoadCharacterData(CharacterData data)
    {
        this.data = data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetFuelPercentage()
    {
        return data.FuelPoints / maxFuelPoints;
    }

    public float GetHullPercentage()
    {
        return data.HullPoints / maxHullPoints;
    }

    public bool IsDestroyed()
    {
        return data.Destroyed;
    }

    public void BurnFuel(float toBurn)
    {
        if (toBurn < 0)
        {
            throw new Exception("Fuel to burn must be a positive number.");
        }

        if (data.FuelPoints - toBurn < 0)
        {
            data.FuelPoints = 0;
            return;
        }

        data.FuelPoints -= toBurn;
    }

    public void Refuel()
    {
        data.FuelPoints = maxFuelPoints;
    }

    public void RepairHull()
    {
        data.HullPoints = maxHullPoints;
    }

    public float GetFuel()
    {
        return data.FuelPoints;
    }

    public float GetMaxFuel()
    {
        return maxFuelPoints;
    }

    public float GetMaxHull()
    {
        return maxHullPoints;
    }

    public void DamageHull(float damage)
    {
        if (damage < 0)
        {
            throw new Exception("Damage to hull must be a positive number.");
        }

        if (data.HullPoints - damage < 0)
        {
            data.HullPoints = 0;
            data.Destroyed = true;
            return;
        }

        data.HullPoints -= damage;
    }

    private void Flip(bool flipX)
    {
        foreach (VehiclePart part in parts)
        {
            part.Flip(flipX);
        }
    }

    public void Move(Vector2 direction)
    {
        if (GetFuelPercentage() == 0 || data.Destroyed)
        {
            return;
        }

        if (direction.x < 0) {
            Flip(true);
            heldTool?.Flip(true);
        } else if (direction.x > 0) {
            Flip(false);
            heldTool?.Flip(false);
        }
        
        if (Math.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(direction * speed);
            BurnFuel(speed / movementFuelDivisor);
        }
    }

    public void Jump()
    {
        if (GetFuelPercentage() == 0 || data.Destroyed)
        {
            return;
        }

        // Make sure we are on the ground first
        if (!rb.IsTouching(terrainFilter)) {
            return;
        }

        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        BurnFuel(jumpFuelCost);

        //aud.Play();
    }

    public void Immolate()
    {
        if (onFire)
        {
            return;
        }

        onFire = true;
        flameOverlay.color = Color.white;
    }

    private IEnumerator FireDamageRoutine()
    {
        while (true)
        {
            if (onFire)
            {
                DamageHull(fireDamagePerSecond * Time.deltaTime);
            }

            yield return null;
        }
    }

    public void Extinguish()
    {
        if (!onFire)
        {
            return;
        }

        onFire = false;
        flameOverlay.color = Color.clear;
    }
}
