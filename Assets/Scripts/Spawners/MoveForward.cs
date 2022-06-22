using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int speed;
    public int direction;
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z <= -1)
        {
            Destroy(gameObject);
        }
    }
}
