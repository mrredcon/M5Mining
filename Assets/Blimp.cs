using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blimp : MonoBehaviour
{
    [SerializeField] private Transform rig;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float leewayX = 2.0f;
    [SerializeField] private ShopHandler shop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowardsRig()
    {
        StartCoroutine(MoveTowardsXRoutine(rig.position.x));
    }

    private IEnumerator MoveTowardsXRoutine(float targetX)
    {
        while (transform.position.x < targetX - leewayX || transform.position.x > targetX + leewayX) {
            if (transform.position.x < targetX) {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else if (transform.position.x > targetX) {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            
            yield return null;
        }
        
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        shop.OpenUI();
    }

    void RaiseRig()
    {

    }

    void LowerRig()
    {

    }
}
