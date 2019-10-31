using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAndRelease : MonoBehaviour
{
    Vector2 startPos;
    //force added upon release
    public float force = 1300f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseUp()
    {
        //disable kinematic
        GetComponent<Rigidbody2D>().isKinematic = false;

        //add the force
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);

        //remove this script
        Destroy(this);
    }

    private void OnMouseDrag()
    {
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //keep in a certain radius
        float radius = 0.5f;
        Vector2 dir = p - startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;

        //set the position
        transform.position = startPos + dir;
    }

}
