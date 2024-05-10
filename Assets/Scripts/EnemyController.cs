using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private new SpriteRenderer renderer;
    private Rigidbody2D rb2d;
    public float speed;
    public float currentSpeed;
    public float horizontal;
    float horizontalDirection = 1f;
    public float vertical;
    int direction;
    bool moving = false;
    bool toggleDirection = false;
    Vector2 movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        vertical = 0f;

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     moving = !moving;
        // }

        if (toggleDirection)
        {
            horizontalDirection = -horizontalDirection;
            toggleDirection = false;
        }

        horizontal = moving ? horizontalDirection : 0f;
        currentSpeed = moving ? speed : 0f;

        // Up
        if (vertical < 0)
        {
            animator.SetBool("Run", true);
            movement = new Vector2(0f, vertical);
        }
        // Left 
        else if (horizontal < 0)
        {
            animator.SetBool("Run", true);
            movement = new Vector2(horizontal, 0f);
            renderer.flipX = true;
        }
        // Up
        else if (vertical > 0)
        {
            animator.SetBool("Run", true);
            movement = new Vector2(0f, vertical);
        }
        // Right
        else if (horizontal > 0)
        {
            animator.SetBool("Run", true);
            movement = new Vector2(horizontal, 0f);
            renderer.flipX = false;
        }
        else
        {
            animator.SetBool("Run", false);
            movement = new Vector2(0f, 0f);
        }

        movement = moving ? movement : new Vector2(0f, 0f);
        rb2d.velocity = movement * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            toggleDirection = true;
        }
    }
}
