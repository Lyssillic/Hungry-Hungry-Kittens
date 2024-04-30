using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumballController : MonoBehaviour
{
    float amp = .1f;
    float frequency = 1f;
    Vector3 initPos;

    void Start() 
    {
        initPos = transform.position;
    }

    void Update()
    {
        // Update gumball position using a sine wave
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * frequency) * amp + initPos.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collider collided with another collider
        // and destroy the gumball if it did
        Destroy(gameObject);
    }
}
