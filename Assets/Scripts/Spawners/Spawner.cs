using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject coin;

    public GameObject enemy;

    private float _nextRespawn = 2f;
    private float time;
    
    float[] positionSpawner = { -1.0F, 0.0F, 1.0F };

    private void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= _nextRespawn)
        {
            Spaw();
        }
    }

    void Spaw()
    {
        var positionX = positionSpawner[Random.Range(0, positionSpawner.Length)];
        Vector3 position = new Vector3(positionX, transform.position.y, transform.position.z);
        Instantiate(coin, position,Quaternion.identity);
            
        var positionX2 = positionSpawner[Random.Range(0, positionSpawner.Length)];
        Vector3 position2 = new Vector3(positionX2, transform.position.y, transform.position.z);
        Instantiate(enemy, position2,Quaternion.identity);
        time = 0.0f;
    }
}
