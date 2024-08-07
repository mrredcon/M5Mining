using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private List<VehiclePart> subParts;
    [SerializeField] private CustomPivot customPivot;

    [SerializeField] private float minAngle = -72.0f;
    [SerializeField] private float maxAngle = 70.0f;
    [SerializeField] private Vehicle parentVehicle;

    private Transform pivotTransform;
    bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        pivotTransform = customPivot.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointAt(Vector3 target)
    {
        if (parentVehicle.IsDestroyed())
        {
            return;
        }
        
        Vector3 pivotPoint = pivotTransform.position;
        Vector3 difference = target - pivotPoint;

        if (flipped)
        {
            difference = pivotPoint - target;
        }

        float theta = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (theta < minAngle)
        {
            theta = minAngle;
        }
        else if (theta > maxAngle)
        {
            theta = maxAngle;
        }

        pivotTransform.eulerAngles = new Vector3(0, 0, theta);
    }

    public void Flip(bool flipX)
    {
        if (flipped != flipX) {
            pivotTransform.eulerAngles = new Vector3(0, 0, pivotTransform.eulerAngles.z * -1);

            foreach (VehiclePart part in subParts)
            {
                part.Flip(flipX);
            }

            customPivot.Flip(flipX);
        }

        flipped = flipX;
    }
}
