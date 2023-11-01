using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Vector3 _dir;
    public float bulletRange;

    private GameObject _target;
    
    private Vector3 _startPos;

    void OnEnable()
    {
        _target = GameObject.Find("FirePosition");
        _startPos = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }

    void Shoot()
    {
        _dir = transform.up.normalized;
        transform.position += _dir * (speed * Time.deltaTime);
    }

    void CheckRange()
    {
        if (Vector3.Distance(transform.position, _startPos) > bulletRange)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        Shoot();
        CheckRange();
    }

    void OnTriggerEnter(Collider enemy)
    {
        Debug.Log("맞았다!");
        gameObject.SetActive(false);
        
    }
}
