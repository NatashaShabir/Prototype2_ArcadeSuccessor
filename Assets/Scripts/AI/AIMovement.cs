using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public bool movingLeft = true; //Is the AI Moving Left?
    public bool isChopping = false;
    private bool isMoving = true; //Is the AI Moving?

    public float speed = 0f; //Speed of Movement
    public int damage = 10;
    public Transform groundDetection; //Point to check for ground from

    private TreeFunctions treeRef; //Reference to script on tree

    public Animator animator; //Reference to Animator on Sprite

    private void Start()
    {
        if (movingLeft == true) //If the player is set to move left at the beginning then making sure he is set to face left
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingLeft = false;
        }
        else //Otherwise flip the AI
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingLeft = false;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            speed = 2f;

            transform.Translate(Vector2.left * speed * Time.deltaTime);  //Make the AI move to it's left by the speed and time

            RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, 10f); //Check is there is ground below the AI
        
            if(groundCheck.collider == false) //If the groundcheck doesn't hit anything then continue code here
            {
                if (movingLeft == true) //If the AI is already moving left we need to flip the direction it is facing
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingLeft = false;
                }
                else //Otherwise reset the direction the AI is facing
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingLeft = false;
                }
            }

        }
        else //If the AI isn't moving at all set the speed to zero
        {
            speed = 0f;

        }

        animator.SetFloat("Speed", speed); //Tells the animator whether the AI is moving or not

    }
        

    private void OnCollisionStay2D(Collision2D col) //Checks what objects the AI collides with
    {
        if (col.gameObject.tag == "Tree") //If the collided object is a tree then continue code here
        {
            isMoving = false;
            isChopping = true;
            animator.SetBool("isChopping", true); //Tell the animator we are now chopping

            treeRef = col.gameObject.GetComponent<TreeFunctions>();

            if (treeRef != null)
            {
                treeRef.damageTaken = damage;
                treeRef.beingChopped = true;
            }
            else
            {
                animator.SetBool("isChopping", false); //Tell the animator we are stopped chopping
            }
        }
    }   
    
}
