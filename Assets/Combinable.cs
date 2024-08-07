using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinable : MonoBehaviour
{
    [SerializeField] private string myName;

    private string[] allowedCombinations = {
        "AnimalAnimal",
        "AnimalMelon",
        "AnimalCrab",
        "AnimalTree",
        "CrabMelon",
        "CrabTree",
        "MelonTree"
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Combine(Combinable other, Vector3 position)
    {
        string combinedName = GetCombinedName(other);
        if (combinedName == null)
        {
            return null;
        }

        return Instantiate(Resources.Load(combinedName, typeof(GameObject)), position, Quaternion.identity) as GameObject;
    }

    private string GetCombinedName(Combinable other)
    {
        // Alpha - myName
        // Zeta - other

        // "Alpha".CompareTo("Zeta")
        // A - Z = -25, leave it alone as AlphaZeta

        // "Zeta".CompareTo("Alpha")
        // Z - A = 25, we should flip it from Zeta.Alpha to AlphaZeta
        
        string mine = GetName();
        string theirs = other.GetName();
        string combinedName;

        if (mine.CompareTo(theirs) > 0)
        {
            combinedName = theirs + mine;
        } else {
            combinedName = mine + theirs;
        }

        if (Array.IndexOf(allowedCombinations, combinedName) >= 0) {
            return combinedName;
        }

        return null;
    }

    public string GetName()
    {
        return myName;
    }
}
