using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbDetector : MonoBehaviour
{
    //public GameObject lumberjack;
    public AIMovement aiScript;
    public Transform raycastPos;
    public LayerMask WhatIsAI;
    
    private void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(raycastPos.position, Vector2.down, 100f, WhatIsAI);

        if(rayHit)
        {
            aiScript = rayHit.collider.GetComponent<AIMovement>();
        }

        if(aiScript != null)
        {
            aiScript.canClimb = true;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {        
        if (other.tag == ("Lumberjack"))
        {
            Physics2D.IgnoreLayerCollision(12, 11, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Lumberjack"))
        {
            Physics2D.IgnoreLayerCollision(12, 11, false);

            if(aiScript != null)
            {
                aiScript.rb.gravityScale = 1;
                aiScript.myState = AIMovement.AiStates.Searching;
                Destroy(gameObject);
            }

        }
        
    }
}
