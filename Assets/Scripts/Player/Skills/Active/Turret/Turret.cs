using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretGunBody = null;
    
    Transform _turretTarget = null;
    
    // 탄환
    private GameObject[] _turretbulletObjectPool;
    public GameObject turretbulletFactory;
    private int _turretBulletPoolSize = 10;
    private GameObject _turretBullet;
    
    public GameObject turretFirePosition;
    
    public float turretAttackDelay = 1f;
    
    private bool _isFiring = false;

    private void Awake()
    {
        GameDataManager.Instance.TurretLevel = 0;
        MakeTurretBullet();
    }

    void Update()
    {
        if (_turretTarget == null)
            turretGunBody.Rotate(new Vector3(0,45,0)*Time.deltaTime);
        else
        {
            turretGunBody.LookAt(_turretTarget);
            if (!_isFiring)
            {
                StartCoroutine(StartFiring());
            }

        }
    }

    void MakeTurretBullet()
    {
        _turretbulletObjectPool = new GameObject[_turretBulletPoolSize];
        
        for (int i = 0; i < _turretBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(turretbulletFactory);
            bullet.SetActive(false);
            _turretbulletObjectPool[i] = bullet;
        }
    }
    
    void OnTriggerStay(Collider collision) // 사거리 내로 적이 들어왔을 때
    {
        if (collision.CompareTag("Enemy"))
        {
            _turretTarget = collision.gameObject.transform;
        }
    }
    
    void OnTriggerExit(Collider other) // 타겟 변경
    {
        _turretTarget = null;
        if (_isFiring)
        {
            StopFiring();
        }
    }

    IEnumerator StartFiring()
    {
        _isFiring = true;
        while (_turretTarget != null)
        {
            TurretFire();
            yield return new WaitForSeconds(turretAttackDelay);
        }
        _isFiring = false;
    }

    private void StopFiring()
    {
        _isFiring = false;
        StopAllCoroutines();
    }

    void TurretFire()
    {
        for (int i = 0; i < _turretBulletPoolSize; i++)
        {
            _turretBullet = _turretbulletObjectPool[i];
            if (_turretBullet.activeSelf == false)
            {
                _turretBullet.SetActive(true);
                _turretBullet.transform.position = turretFirePosition.transform.position;
                break;
            }
        }
    }
}