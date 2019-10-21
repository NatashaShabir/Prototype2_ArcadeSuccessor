using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MyCharacterController controller;

    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
   // private Vector3 touchPosition;

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
            if (Input.touchCount == 1)
            {
                if (touchPosition != transform.position)
                {
                    float x = touchPosition.x;
                    float y = touchPosition.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x < 0)
                        {
                            touchPosition += Vector3.left;
                        }
                        else
                        {
                            touchPosition += Vector3.right;
                        }

                        transform.position = Vector3.MoveTowards(transform.position, touchPosition, runSpeed * Time.fixedDeltaTime);
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
