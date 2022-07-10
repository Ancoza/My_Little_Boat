using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Coin coinPrefab;

    public Enemy[] allEnemies;

    public Transform coins;
    public Transform enemies;

    readonly float[] _positionSpawner = { -1.0F, 0.0F, 1.0F };

    private float _rateEnemy = 2.0f, _rateCoins = 0.5f, _timeScale = 5, _less = 0.25f;

    private void Start()
    {
        InvokeRepeating(nameof(GenerateEnemy),0,_rateEnemy);
        InvokeRepeating(nameof(GenerateCoin),0,_rateCoins);
        InvokeRepeating(nameof(IncrementDifficult) , _timeScale, _timeScale);
    }
    
    void IncrementDifficult()
    {
        _rateCoins += _less;
        _rateEnemy += _less;
    }
    float GetPositionSpawner()
    {
        float position = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
        return position;
    }

    void GenerateEnemy()
    {
        int enemiesCount = Random.Range(0, 3);
        if (enemiesCount == 2)
        {
            List<float> pos = new List<float>();
            float num;
            do
            {
                num = GetPositionSpawner();
                pos.Add(num);
            } while (pos.Contains(num) && pos.Count < 2);

            for (int i = 0; i < enemiesCount; i++)
            {
                int idx = Random.Range(0, allEnemies.Length);
                Vector3 position = new Vector3(pos[i], 0, 40);
                Enemy enemy = Instantiate(allEnemies[idx], enemies, false);
                enemy.transform.position = position;
            }
           
            
        }
        else
        {
            int idx = Random.Range(0, allEnemies.Length);
            var positionX = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
            Vector3 position = new Vector3(positionX, 0, 40);
            Enemy enemy;
            enemy = Instantiate(allEnemies[idx], enemies, false);
            enemy.transform.position = position;
        }
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
