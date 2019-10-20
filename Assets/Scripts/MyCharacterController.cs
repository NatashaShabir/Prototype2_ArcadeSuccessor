using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
    [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck; // A position marking where to check for ceilings

    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D Rigidbody2D;
	private bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    
    public void Move (float movement, bool jump)
    {
        if (grounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(movement * 10f, Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (movement > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (movement < 0 && facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
           Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
