using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private Vector3 _dir;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.LookAt(transform.position + _dir);
        _dir = target.transform.position - transform.position;
        _dir.Normalize();
        transform.position += _dir * (5 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("총알에 맞음");
        }
        if (other.gameObject.CompareTag("Axe"))
        {
            Debug.Log("도끼에 맞음");
        }
    }
}
