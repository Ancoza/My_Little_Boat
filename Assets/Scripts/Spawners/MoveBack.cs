using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    float _speed;
    
    [SerializeField] 
    float pointDestruction;
    void Update()
    {
        _speed = GameManager.SharedInstance.GetGameVelocity();
        
        transform.Translate(Vector3.back * (_speed * Time.deltaTime));
        if (transform.position.z <= pointDestruction)
        {
            Destroy(gameObject);
        }
    }
}
