using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource aud;

    private ContactFilter2D terrainFilter = new();

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Tool heldTool = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();

        terrainFilter.layerMask = LayerMask.NameToLayer("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction)
    {
        if (direction.x < 0) {
            sr.flipX = true;
            heldTool?.Flip(true);
        } else if (direction.x > 0) {
            sr.flipX = false;
            heldTool?.Flip(false);
        }
        
        if (Math.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(direction * speed);
        }
    }

    public void Jump()
    {
        // Make sure we are on the ground first
        if (!rb.IsTouching(terrainFilter)) {
            return;
        }

        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        aud.Play();
    }
}
