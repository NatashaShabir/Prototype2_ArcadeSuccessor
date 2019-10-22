using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MyCharacterController controller;

    public float runSpeed = 40f;
    public float jumpForce = 25f;
    private float horizontalMove = 0f;
    private bool jump = false;

    public Rigidbody2D myRigidbody;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
            Debug.Log(touchPosition);
            if (Input.touchCount == 1)
            {
                float x = touchPosition.x;
                float y = touchPosition.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    touchPosition.z = 0f;
                    //touchPosition.y = transform.position.y;

                    if (x < 0)
                    {
                        touchPosition += Vector3.left;
                    }
                    else
                    {
                        touchPosition += Vector3.right;
                    }

                    transform.position = Vector3.Lerp(transform.position, touchPosition, runSpeed * Time.fixedDeltaTime);
                }

                if (Mathf.Abs(y) > 1)
                {
                    touchPosition.z = 0f;
                    touchPosition.x = transform.position.x;

                    if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f))
                    {
                        jump = true;
                    }    

                        // touchPosition += Vector3.up;

                    if (jump == true)
                    {
                        if (y < 0)
                        {
                            touchPosition += Vector3.down;
                        }
                        else
                        {
                            touchPosition += Vector3.up;
                        }

                        transform.position = Vector3.Lerp(transform.position, touchPosition, Time.fixedDeltaTime);
                        jump = false;
                    }
                }
                
            }
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPosition.z = 0f;

        //}
    }

    private void FixedUpdate()
    {
        //controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
