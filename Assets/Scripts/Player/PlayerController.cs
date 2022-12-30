using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI tmpCoins;

    [Header("Effects")]
    public GameObject explosion;
    public GameObject bullet;
    public GameObject parent;
    public GameObject boatParent;

    [Header("Stella")] 
    public ParticleSystem a;
    public ParticleSystem b;
    
    [Header("Player")][SerializeField]
    private float velocityMove;

    private float coldownBullet = 3.0f;
    private float timer = 0.0f;
    
    private float _horizontalMove;
    private float _verticalMove;
    private float _limX;
    private float _score;
    
    private Animator anim;
    private float _screenWidth;
    private Player _player;

    public GameObject FireBullet;
    private void Awake()
    {
        _screenWidth = Screen.width;
        _limX = 1f;
        _player = gameObject.GetComponent<Player>();
        anim = _player.gameObject.GetComponentInChildren<Animator>();
        anim.SetBool("alive", true);
        anim.SetBool("move", false);
    }

    void Start()
    {
        //Instantiate(GameManager.SharedInstance.GetBoat().gameObject, boatParent.transform);
    }

    void Update()
    {
        timer += Time.deltaTime;
        KeepPlayerInside();

        FireBullet.SetActive(timer >= coldownBullet);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        timer = 0.0f;
        Vector3 pos = new Vector3(transform.position.x, -1, 2);
        Instantiate(bullet,pos,quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _player.AddCoin();
        }
        if (other.CompareTag("Enemy"))
        {
            GameObject fx = Instantiate(explosion, parent.transform, false);
            fx.transform.position = transform.position;
            a.Stop();
            b.Stop();
            anim.SetBool("alive", false);
            GameManager.SharedInstance.GameOver();
            //_player.AddScore(GameManager.SharedInstance.GetDistance());
            _player.SavePlayer();
            //_player.SumCoins();
        }
    }
    
    public void MoveGyro()
    {
        float direction;
#if UNITY_EDITOR
        direction = Input.GetAxis("Horizontal");
#else
        Input.gyro.enabled = true;
        direction = Input.gyro.rotationRate.y;
#endif
        if (direction > 0.1f)
        {
            anim.SetBool("move", true);
            anim.SetFloat("direction", 1.0f);
        }
        else if (direction < -0.1f)
        {
            anim.SetBool("move", true);
            anim.SetFloat("direction", -1.0f);
        }else
        {
            anim.SetBool("move", false);
            anim.SetFloat("direction", 0.0f);
        }
        transform.Translate(Vector3.right * direction * velocityMove * Time.deltaTime);
    }

    public void MoveTouch()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > _screenWidth / 2)
            {
                //transform.Translate(Vector3.right * velocityMove * Time.deltaTime);
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    anim.SetBool("move", true);
                    anim.SetFloat("direction", 1.0f);
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    anim.SetBool("move", false);
                    anim.SetFloat("direction", 0.0f);
                }
            }

            if (Input.GetTouch(i).position.x < _screenWidth / 2)
            {
                //transform.Translate(Vector3.left * velocityMove * Time.deltaTime);
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    anim.SetBool("move", true);
                    anim.SetFloat("direction", -1.0f);
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    anim.SetBool("move", false);
                    anim.SetFloat("direction", 0.0f);
                }
            }
            ++i;
        }
    }

    public void MoveRight()
    {
        anim.SetBool("move", false);
        transform.Translate(Vector3.right);
    }

    public void MoveLeft()
    {
        anim.SetBool("move", false);
        transform.Translate(Vector3.left);
    }
    
    public void AnimRight()
    {
        anim.SetBool("move", true);
        anim.SetFloat("direction", 1.0f);

    }

    public void AnimLeft()
    {
        anim.SetBool("move", true);
        anim.SetFloat("direction", -1.0f);
    }

    private void KeepPlayerInside()
    {
        //Keep player inside limits
        Vector3 currentPosition = transform.position;
        if (currentPosition.x >= _limX)
        {
            transform.position = new Vector3(_limX, currentPosition.y, currentPosition.z);
        }
        else if (currentPosition.x <= -_limX)
        {
            transform.position = new Vector3(-_limX, currentPosition.y, currentPosition.z);
        }
    }
}
