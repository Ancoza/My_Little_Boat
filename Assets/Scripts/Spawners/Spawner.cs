using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Coin coinPrefab;

    public Enemy[] allEnemies;

    public Transform coins;
    public Transform enemies;

    private float _nextRespawn = 2f;
    private float _time;

    readonly float[] _positionSpawner = { -1.0F, 0.0F, 1.0F };

    private void Start()
    {
        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _nextRespawn)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GenerateCoin();
        GenerateEnemy();
        
        _time = 0.0f;
    }

    void GenerateEnemy()
    {
        int idx = Random.Range(0, allEnemies.Length);
        var positionX = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
        Vector3 position = new Vector3(positionX, 0, 40);
        Enemy enemy;
        
        enemy = Instantiate(allEnemies[idx], enemies, false);
        enemy.transform.position = position;
    }

    void GenerateCoin()
    {
        var positionX = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
        Vector3 position = new Vector3(positionX, 0, 40);
        Coin coin;
        
        coin = Instantiate(coinPrefab, coins, false);
        coin.transform.position = position;
    }
}
