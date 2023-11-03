using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed = 20.0f;
    Vector3 _dir;
    private float _arrowRange = 25.0f;

    GameObject _target;
    
    Vector3 _startPos;

    void OnEnable()
    {
        _target = GameObject.FindWithTag("Player");
        _startPos = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }

    void Shoot()
    {
        _dir = transform.forward.normalized;
        transform.position += _dir * (_speed * Time.deltaTime);
    }

    void CheckRange()
    {
        if (Vector3.Distance(transform.position, _startPos) > _arrowRange)
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
            enemy.TakeDamage(GameDataManager.Instance.ArrowDamage);
            gameObject.SetActive(false);
        }
    }
}
