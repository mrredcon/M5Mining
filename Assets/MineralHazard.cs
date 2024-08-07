using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralHazard : MonoBehaviour
{
    private Mineral parentMineral;
    private MineralType mineralType;
    //private Dictionary<Collider2D, float> occupants;

    void Awake()
    {
        //occupants = new Dictionary<Collider2D, float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        parentMineral = transform.parent.GetComponent<Mineral>();

        // Chunk of earth did not have a MineralType assigned to it, bail out
        if (parentMineral == null)
        {
            Shutdown();
            return;
        }

        mineralType = parentMineral.GetMineralType();

        // Go to sleep if the parent mineral isn't a threat
        if (mineralType == null || !mineralType.Dangerous)
        {
            Shutdown();    
            return;
        }
    }

    private void Shutdown()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (mineralType == null)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player")) {
            VehiclePart vehiclePart = collision.gameObject.GetComponent<VehiclePart>();
            Vehicle vehicle = vehiclePart.GetVehicle();
            vehicle.Immolate();
        }
    }

    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (mineralType == null)
    //     {
    //         return;
    //     }

    //     if (occupants.ContainsKey(collision)) {
    //         occupants[collision] += Time.deltaTime;
    //         if (occupants[collision] >= 1.0f) {
    //             VehiclePart vehiclePart = collision.gameObject.GetComponent<VehiclePart>();
    //             Vehicle vehicle = vehiclePart.GetVehicle();

    //             if (!vehicle.IsDestroyed()) {
    //                 vehicle.DamageHull(mineralType.DamagePerSecond);
    //             }
                
    //             occupants[collision] = 0.0f;
    //         }
    //     }
    // }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (mineralType == null)
        {
            return;
        }

        //occupants.Remove(collision);
        //if (mineralType.Immolate)
        //{
        if (collision.gameObject.CompareTag("Player"))
        {
            VehiclePart vehiclePart = collision.gameObject.GetComponent<VehiclePart>();
            Vehicle vehicle = vehiclePart.GetVehicle();
            vehicle.Extinguish();
        }
        //}
    }
}
