using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")] 
    public Coin coinPrefab;
    public Tree tree;
    public Enemy[] allEnemies;
    public GameObject shark;

    public Transform coins;
    public Transform enemies;

    readonly float[] _positionSpawner = { -1.0F, 0.0F, 1.0F };

    //[Header("Spawner settings")] 
    private float _rateEnemy = 1.5f;
    private float _rateCoins = 0.5f;
    private float _timeScale = 5;
    private float _less = 0.05f;

    public void StartSpawner()
    {
        StartCoroutine(nameof(GenerateEnemies));
        InvokeRepeating(nameof(GenerateCoin),0,_rateCoins);
        InvokeRepeating(nameof(IncrementDifficult) , _timeScale, _timeScale);
        InvokeRepeating(nameof(GenerateTree) , Random.Range(5,10), 10);
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
        int enemiesCount = Random.Range(0, 5);
        
        if (enemiesCount == 0)
        {
            int idx = Random.Range(0, allEnemies.Length);
            var positionX = GetPositionSpawner();
            Vector3 position = new Vector3(positionX, -0.9f, 40);
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
            
            for (int i = 0; i < 2; i++)
            {
                var position = i == 0 ? new Vector3(pos1, -0.9f, 40) : new Vector3(pos2,-0.9f,40);
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

    void GenerateTree()
    {
        int random = Random.Range(0, 2);
        if(random > 0)
        {
            Vector3 positionTree = new Vector3(2.5f, -0.5f, 15f);
            Tree treefall = Instantiate(tree, enemies, false);
            treefall.transform.position = positionTree;
        }
        else
        {
            Vector3 positionTree = new Vector3(-2.5f, -0.5f, 15f);
            Tree treefall = Instantiate(tree, enemies, false);
            treefall.transform.position = positionTree;
        }
    }

    public void GenerateReward()
    {
        int randomReward = Random.Range(0, 4);
        Debug.Log(randomReward);
        switch (randomReward)
        {
            case 0:
                Debug.Log("YOU WIN 10 COINS");
                break;
            case 1:
                Debug.Log("YOU WIN 1 HELM");
                break;
            case 2:
                Debug.Log("YOU WIN NOTHING");
                break;
            case 3:
                Debug.Log("YOU WIN A SHARK?");
                break;
            default:
                Debug.Log("Default case");
                break;
        }
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
