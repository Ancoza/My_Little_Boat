using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    public Button btnR;
    public Button btnL;
    private Player _player;
    
    
    private float _screenWidth;

    void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _limX = 1.5f;
        _screenWidth = Screen.width;
        Instantiate(GameManager.SharedInstance.GetBoat().gameObject, boatParent.transform);
    }

    void Update()
    {
    
#if UNITY_EDITOR
        Debug.Log("Hola");
        if (GameManager.SharedInstance.currentGameState == GameState.InGame && 
            GameManager.SharedInstance.currentGameController == GameController.Gyroscope)
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * _horizontalMove * velocityMove * Time.deltaTime);

        }else if(GameManager.SharedInstance.currentGameState == GameState.InGame && 
                 GameManager.SharedInstance.currentGameController == GameController.Touch)
        {
            Debug.Log("Hola2");
            int i = 0;
            while (i < Input.touchCount)
            {
                if (Input.GetTouch(i).position.x > _screenWidth / 2)
                {
                    MoveSides(1.0f);
                }
                if (Input.GetTouch(i).position.x < _screenWidth / 2)
                {
                    MoveSides(-1.0f);
                }
                ++i;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveSystem.Delete();
        }
#else
        Debug.Log("Hola2");
        if (GameManager.SharedInstance.currentGameState == GameState.InGame && 
            GameManager.SharedInstance.currentGameController == GameController.Gyroscope)
        {
            Input.gyro.enabled = true;
            _horizontalMove = Input.gyro.rotationRate.y;
            //velocityMove = Input.gyro.userAcceleration.z;
        
            transform.Translate(Vector3.right * _horizontalMove * velocityMove * Time.deltaTime);
        }else if(GameManager.SharedInstance.currentGameState == GameState.InGame && 
            GameManager.SharedInstance.currentGameController == GameController.Touch)
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                if (Input.GetTouch(i).position.x > _screenWidth / 2)
                {
                    player.GetComponent<PlayerController>().MoveSides(1.0f);
                }
                if (Input.GetTouch(i).position.x < _screenWidth / 2)
                {
                    player.GetComponent<PlayerController>().MoveSides(-1.0f);
                }
                ++i;
            }
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

    public void MoveSides(float horizontalInput)
    {
        if (GameManager.SharedInstance.currentGameState == GameState.InGame)
        {
            transform.Translate(Vector3.right * horizontalInput * velocityMove * Time.deltaTime);
        }
    }
    
}
