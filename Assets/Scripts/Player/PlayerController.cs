using TMPro;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;

    public TextMeshProUGUI attitude, enable, gravity, rr, rru, updateInterval, userAcceleration;

    private float _horizontalMove;
    private float _verticalMove;
    [SerializeField]
    private float _velocityMove;

    public float limX;
    

    void Update()
    {
    
    #if UNITY_EDITOR
        _horizontalMove = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * _horizontalMove * _velocityMove * Time.deltaTime);
        Debug.Log(_horizontalMove);
        
    #else
        
        Input.gyro.enabled = true;
        /*
        attitude.text = "attitude: " +  Input.gyro.attitude;
        enable.text ="enabled: " +   Input.gyro.enabled;
        gravity.text = "Gravity: " +  Input.gyro.gravity;
        rr.text = "RotationRate: " +  Input.gyro.rotationRate;
        rru.text = "RotationRateUnbiased" +  Input.gyro.rotationRateUnbiased;
        updateInterval.text = "Update Interval: " +  Input.gyro.updateInterval;
        userAcceleration.text = "User Acceleration: " +  Input.gyro.userAcceleration;
        */
        _verticalMove = Input.GetAxis("Vertical"); 
        transform.Translate(Vector3.forward * _verticalMove * _velocityMove* Time.deltaTime);
        
        //_horizontalMove = Input.GetAxis("Horizontal");
        _horizontalMove = Input.gyro.rotationRate.y;
        //_velocityMove = Input.gyro.userAcceleration.z;
        
        transform.Translate(Vector3.right * _horizontalMove * _velocityMove * Time.deltaTime);

        Vector3 pos = gameObject.transform.position;
        if (pos.x >= limX)
        {
            transform.position = new Vector3(limX, pos.y, pos.z);
        }
        else if (pos.x <= -limX)
        {
            transform.position = new Vector3(-limX, pos.y, pos.z);
        }
        
     #endif
    }

}
