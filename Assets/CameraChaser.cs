using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaser : MonoBehaviour
{
    [SerializeField] private float yOffset = 4;
    [SerializeField] private float zPosition = -10;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject toChase;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = toChase.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(tf.position.x, tf.position.y + yOffset, zPosition);
    }
}
