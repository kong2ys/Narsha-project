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
}
