using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MForward : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
