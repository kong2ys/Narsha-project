using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLimit : MonoBehaviour
{
    public ParticleSystem ring;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Wall"))
        {
            ring.Play();
        }
    }
}
