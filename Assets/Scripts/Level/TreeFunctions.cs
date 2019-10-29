using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFunctions : MonoBehaviour
{
    public int health = 200;
    public int damageTaken = 0;

    public float time = 0f;
    public float timeReset = 2f;

    public bool beingChopped = false;


    public void Update()
    {
        if (time >= 0)
        {
            time -= Time.fixedDeltaTime;
        }
        else if (time <= 0)
        {
            time = timeReset;
            if (beingChopped)
            {
                TakeDamage(damageTaken);

            }
        }
    }

    public void TakeDamage(int damage)
    {

        if(health > 0)
        {
            health -= damage;
            Debug.Log(health);
        }
        else if(health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
