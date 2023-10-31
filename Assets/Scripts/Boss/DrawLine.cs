using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private float speed = 40;
    // Update is called once per frame
    void Update()
    {
        GameObject balltan = GameObject.FindWithTag("Boss");
        transform.rotation = balltan.transform.rotation;
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Wall"))
            Destroy(gameObject, 0.1f);
    }
}
