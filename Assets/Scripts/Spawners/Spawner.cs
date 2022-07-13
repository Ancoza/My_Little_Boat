using System.Collections;
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
        StartCoroutine(nameof(GenerateEnemies));
        InvokeRepeating(nameof(GenerateCoin),0,_rateCoins);
        InvokeRepeating(nameof(IncrementDifficult) , _timeScale, _timeScale);
    }
    
    void IncrementDifficult()
    {
        if (_rateEnemy <= 0.5f)
        {
            _rateEnemy = 0.5f;
        }
        else
        {
            _rateCoins -= _less;
            _rateEnemy -= _less;
        }
    }
    float GetPositionSpawner()
    {
        float position = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
        return position;
    }

    void GenerateEnemy()
    {
        int enemiesCount = Random.Range(0, 3);
        Debug.Log("enemy range: "+enemiesCount);
        if (enemiesCount == 0)
        {
            int idx = Random.Range(0, allEnemies.Length);
            var positionX = GetPositionSpawner();
            Vector3 position = new Vector3(positionX, -0.5f, 40);
            Enemy enemy = Instantiate(allEnemies[idx], enemies, false);
            enemy.transform.position = position;
            
        }
        else
        {
            float pos1, pos2;
            do
            {
                pos1 = GetPositionSpawner();
                pos2 = GetPositionSpawner();
            } while (pos1.Equals(pos2));
            Debug.Log(pos1 + ":" + pos2);
            for (int i = 0; i < enemiesCount; i++)
            {
                var position = i == 0 ? new Vector3(pos1, -0.5f, 40) : new Vector3(pos2,-0.5f,40);
                int idx = Random.Range(0, allEnemies.Length);
                Enemy enemy = Instantiate(allEnemies[idx], enemies, false);
                enemy.transform.position = position;
            }
        }
    }

    void GenerateCoin()
    {
        var positionX = _positionSpawner[Random.Range(0, _positionSpawner.Length)];
        Vector3 position = new Vector3(positionX, -0.2f, 40);
        Coin coin;
        
        coin = Instantiate(coinPrefab, coins, false);
        coin.transform.position = position;
    }
    
    IEnumerator GenerateEnemies()
    {
        while( true )
        {
            Invoke(nameof(GenerateEnemy),0);
            yield return new WaitForSeconds(_rateEnemy);
        }
    }
}
