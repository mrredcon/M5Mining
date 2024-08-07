using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPivot : MonoBehaviour
{
    bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveAlongX(float amount)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + amount, transform.localPosition.y, transform.localPosition.z);
    }

    public void Flip(bool flipX)
    {
        if (flipped != flipX) {
            Vector3 localPos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPos.x * -1, localPos.y, localPos.z);
        }
        
        flipped = flipX;
    }
}
