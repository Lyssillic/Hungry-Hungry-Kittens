using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private new SpriteRenderer renderer;
    private Rigidbody2D rb2d;
    public float speed = 1f;
    float horizontal;
    float vertical;
    int direction;
    Vector2 movement;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        // Check if player is moving
        bool isMoving = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f;

        // Update the direction the player is facing if moving
        if (isMoving)
        {
            // Set the running animation
            animator.SetBool("Run", true);

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            movement = new Vector2(horizontal, vertical).normalized;

            // Update the direction the player is facing
            if (vertical < 0)
            {
                renderer.flipX = false;
            }
            else if (vertical > 0)
            {
                renderer.flipX = true;
            }
            else if (horizontal != 0)
            {
                // For left and right movement, flip the sprite based on the horizontal input
                renderer.flipX = horizontal < 0;
            }

            // Allow the player to move freely
            rb2d.velocity = movement * speed;

            // Attack when space is pressed if not currently attacking  
            if (!isAttacking && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackAnimation());
            }
        }
        else
        {
            // Set the running animation to false when not moving
            animator.SetBool("Run", false);

            // Stop the player when not moving
            rb2d.velocity = Vector2.zero;

            // Attack when space is pressed if not currently attacking  
            if (!isAttacking && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackAnimation());
            }
        }
    }


    IEnumerator AttackAnimation()
    {
        isAttacking = true; // Set flag to indicate attacking
        animator.SetBool("Attack", true); // Trigger attack animation
        
        // Stop running during attack animation
        animator.SetBool("Run", false);

        yield return new WaitForSeconds(0.5f); // Adjust the time according to your attack animation
        
        animator.SetBool("Attack", false); // Reset attack animation

        // Resume running after attack animation
        animator.SetBool("Run", true);
        
        yield return new WaitForSeconds(0.05f); // Additional time for animation to complete before allowing another attack
        isAttacking = false; // Reset flag to indicate attack animation is complete
    }

    IEnumerator HurtAnimation(Animator animator)
    {
        animator.SetBool("Hurt", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hurt", false);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(isAttacking);
        if (collision.gameObject.tag == "Enemy" && isAttacking)
        {
            var animator = collision.gameObject.GetComponent<Animator>();
            StartCoroutine(HurtAnimation(animator));
        }
    }
}
