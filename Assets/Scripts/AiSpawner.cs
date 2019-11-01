using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
    public GameObject lumberJack; //What to spawn

    public Transform[] spawnPoints; //Where to spawn
    public int index = 0;

    public float timeDelay = 5f;
    private float timer;


    private void Start()
    {
        timer = timeDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = timeDelay;
            index++;
            SpawnAI();
        }

    }

    private void SpawnAI()
    {
        if (index < spawnPoints.Length)
        {
            GameObject go = Instantiate(lumberJack);

            go.transform.position = spawnPoints[index].position;
        }
        else
        {
            index = 0;
        }
    }
}
