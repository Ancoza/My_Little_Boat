using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public int direction;
    void Update()
    {
        speed = GameManager.SharedInstance.GetGameVelocity();
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z <= -1)
        {
            Destroy(gameObject);
        }
    }
}
