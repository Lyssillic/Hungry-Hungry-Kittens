using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer renderer;
    private Rigidbody2D rb2d;
    public float speed = 1f;
    float horizontal;
    float vertical;
    int direction;
    Vector2 movement;
    // public float jump = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetButtonDown("Jump"))
        // {
        //     rb2d.AddForce(new Vector2(rb2d.velocity.x, jump));
        // }

        // Attack
        // if (Input.GetButtonDown(KeyCode.Space))
        // {
            
        // }

        // Set the player input variables
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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

        // Allow the player to move freely
        rb2d.velocity = movement * speed;
    }
}
