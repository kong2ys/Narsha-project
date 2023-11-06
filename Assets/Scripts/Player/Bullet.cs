using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 40.0f;
    Vector3 _dir;
    public float bulletRange;

    GameObject _target;
    
    Vector3 _startPos;

    void OnEnable()
    {
        _target = GameObject.Find("FirePosition");
        _startPos = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }

    void Shoot()
    {
        _dir = transform.up.normalized;
        transform.position += _dir * (_speed * Time.deltaTime);
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

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GameDataManager.Instance.FireDamage);
            gameObject.SetActive(false);
        }
        
    }
}
