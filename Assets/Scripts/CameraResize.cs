using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResize : MonoBehaviour
{
    public BoxCollider2D backgroundSize;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = backgroundSize.bounds.extents.x / Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
