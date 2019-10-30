using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public bool movingLeft = true; //Is the AI Moving Left?
    public bool isChopping = false;
    private bool isMoving = false;
    private bool isClimbing = false;

    public float speed = 2f; //Speed of Movement
    public int damage = 10;
    public Transform groundDetection; //Point to check for ground from

    private TreeFunctions treeRef; //Reference to script on tree
    public GameObject ladder;
    private GameObject placedLadder;

    public Animator animator; //Reference to Animator on Sprite

    public enum AiStates
    {
        Searching,
        Chopping,
        Climbing
    }

    public AiStates myState;

    private void Start()
    {
        myState = AiStates.Searching;

        if (movingLeft == true) //If the player is set to move left at the beginning then make sure he is set to face left
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else //Otherwise flip the AI
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    void Update()
    {
        animator.SetBool("isChopping", isChopping); //Tell the animator whether the AI is chopping or not
        animator.SetBool("isMoving", isMoving); //Tells the animator whether the AI is moving or not
        animator.SetBool("isClimbing", isClimbing); //Tells the animator whether the AI is climbing the ladder or not

        HandleStates();

    }

    private void HandleStates()
    {
        switch (myState)
        {
            case AiStates.Searching:

                FindingTree();
                isMoving = true;
                isClimbing = false;
                isChopping = false;

                    break;

            case AiStates.Chopping:

                ChoppingTree();
                isMoving = false;
                isChopping = true;

                break;

            case AiStates.Climbing:

                ClimbingLadder();
                isChopping = false;
                isClimbing = true;

                break;
        }
    }

    private void FindingTree()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);  //Make the AI move to it's left by the speed and time

        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, 10f); //Check is there is ground below the AI

        if (groundCheck.collider == false) //If the groundcheck doesn't hit anything then continue code here
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


    private void OnCollisionEnter2D(Collision2D col) //Checks what objects the AI collides with
    {
        if (col.gameObject.tag == "Tree")//If the collided object is a tree then continue code here
        {
            treeRef = col.gameObject.GetComponent<TreeFunctions>();
            myState = AiStates.Chopping; //Sets AI state to chopping
        }
    }   

    private void ChoppingTree()
    {
        if (treeRef != null)
        {
            treeRef.damageTaken = damage;
            treeRef.beingChopped = true;
        }
        else
        {
            myState = AiStates.Climbing;
        }
    }

    private void ClimbingLadder()
    {
        if(placedLadder == null)
        {
            placedLadder = Instantiate(ladder, transform.position, transform.rotation);
        }
    }
}
