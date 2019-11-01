using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //banana prefab to spawn
    public GameObject bananaPrefab;
    //is there a banana in the area?
    bool occupied = false;

    void FixedUpdate()
    {
        //bird not in trigger area anymore?
        if (!occupied)
            spawnNext();
    }

    void spawnNext()
    {
        //spawn a banana at current position
        Instantiate(bananaPrefab, transform.position, Quaternion.identity);
        occupied = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //banana left the monkey
        occupied = false;
    }

}
