using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Player Speed
    public float speed;
    public Vector3 maxSpeed;

    //Jumping
    public float jumpForce;
    public float wallJumpForce;
    public float sideWallJumpForce;
    public float fallMulitplier;
    public float lowJumpMulitplier;
    public float maxVelocity;
    public Vector2 currentVelocity;
    public bool grounded;

    //Components
    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        currentVelocity = rb.velocity;
        if (currentVelocity.x < -10)
        {
            currentVelocity.x = -10;
        }
        else if (currentVelocity.x > 10)
        {
            currentVelocity.x = 10;
        }

        //Sideways Movement
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
        }

        //Jumping
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);
            grounded = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMulitplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulitplier - 1) * Time.deltaTime;
        }

        //MousePos for sprite facing
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float mousePosX = mousePos.x - transform.position.x;

        if (mousePosX >= transform.position.x)
        {
            transform.localScale = new Vector3(.03f, .03f, 1);
        }
        else
        {
            transform.localScale = new Vector3(-.03f, .03f, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
            bool touchingRight = contactPoint.x < center.x;
            bool touchingLeft = contactPoint.x > center.x;

            if (touchingRight && Input.GetKey(KeyCode.W) && !grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x - sideWallJumpForce, rb.velocity.y + wallJumpForce);
            }
            else if (touchingLeft && Input.GetKey(KeyCode.W) && !grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x + sideWallJumpForce, rb.velocity.y + wallJumpForce);
            }
        }
    }
}
