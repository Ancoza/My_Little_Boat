using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI tmpScore, tmpCoins;
    
    [SerializeField]
    private float velocityMove;
    private float _horizontalMove;
    private float _verticalMove;
    private float _limX;
    
    public GameObject explosion;
    public GameObject parent;
    public GameObject boatParent;

    private Player _player;

    void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _limX = 1.5f;
        Instantiate(GameManager.SharedInstance.GetBoat().gameObject, boatParent.transform);
    }

    void Update()
    {
    
#if UNITY_EDITOR
        if (GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * _horizontalMove * velocityMove * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveSystem.Delete();
        }
#else
        if (GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            Input.gyro.enabled = true;
            _horizontalMove = Input.gyro.rotationRate.y;
            //velocityMove = Input.gyro.userAcceleration.z;
        
            transform.Translate(Vector3.right * _horizontalMove * _velocityMove * Time.deltaTime);
        }
#endif
        Vector3 pos = gameObject.transform.position;
        if (pos.x >= _limX)
        {
            transform.position = new Vector3(_limX, pos.y, pos.z);
        }
        else if (pos.x <= -_limX)
        {
            transform.position = new Vector3(-_limX, pos.y, pos.z);
        }

        tmpCoins.text = "" + _player.GetCoins();
        tmpScore.text = "" + _player.GetScore().ToString("F");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _player.AddCoin();
        }
        if (other.CompareTag("Enemy"))
        {
            GameObject fx = Instantiate(explosion,parent.transform,false);
            fx.transform.position = transform.position;
            _player.gameObject.GetComponentInChildren<Animation>().Play();
            GameManager.SharedInstance.GameOver();
            _player.SumCoins();
        }
    }
    
    
    
    
}
