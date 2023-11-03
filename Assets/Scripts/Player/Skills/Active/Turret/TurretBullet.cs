using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float speed;
    private Vector3 _dir;
    public float bulletRange;

    private GameObject _target;
    
    private Vector3 _startPos;

    void Update()
    {
        Shoot();
        CheckRange();
    }
    
    void OnEnable()
    {
        _target = GameObject.Find("TurretFirePosition");
        _startPos = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }

    void Shoot()
    {
        _dir = transform.forward.normalized;
        transform.position += _dir * (speed * Time.deltaTime);
    }

    void CheckRange()
    {
        if (Vector3.Distance(transform.position, _startPos) > bulletRange)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GameDataManager.Instance.TurretDamage);
            Debug.Log("맞았다!");
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}