using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VehiclePart : MonoBehaviour
{
    private Vector3 originalPosition;
    private SpriteRenderer sprite;
    
    [SerializeField] private SpriteRenderer foregroundSprite = null;
    bool flipped = false;
    [SerializeField] private Vehicle parentVehicle;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vehicle GetVehicle()
    {
        return parentVehicle;
    }

    public void Flip(bool flipX)
    {
        if (flipped != flipX)
        {
            if (flipX)
            {
                gameObject.transform.localPosition = new Vector3(originalPosition.x * -1, originalPosition.y, originalPosition.z);
            }
            else
            {
                gameObject.transform.localPosition = originalPosition;
            }

            if (sprite != null)
            {
                sprite.flipX = flipX;
            }

            if (foregroundSprite != null)
            {
                foregroundSprite.flipX = flipX;
            }
        }

        flipped = flipX;
    }
}
