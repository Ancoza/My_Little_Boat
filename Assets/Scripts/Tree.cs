using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 0)
        {
            anim.SetInteger("direction",1);
        }
        else
        {
            anim.SetInteger("direction", -1);
        }
    }
}
