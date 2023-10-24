using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Drone : MonoBehaviour
{
    [SerializeField] GameObject droneMissileFactory = null;//미사일 프리팹
    [SerializeField] Transform droneMissilePosition = null;//미사일 발사 위치
    public float jumpSpeed = 5f;
    
    public Transform target = null;

    private GameObject[] _droneMissileObjectPool;
    private int _missilePoolSize = 50;
    private GameObject _droneMissile;
    
    private float[] _missileAttackDelay = {1.0f, 0.5f, 0.2f, 0.07f, 0.01f};

    private bool _isajtlrl = true;

    private Collider _collider;
    
    public Transform floowTarget;
    public Vector3 offset;

    void Awake()
    {
        GameDataManager.Instance.DroneLevel = 0;
        
        _collider = GetComponent<Collider>();

        MakeDronBullet();
    }

    void Update()
    {
        transform.position = (floowTarget.position + offset);
        transform.LookAt(target);
    }
    
    void MakeDronBullet()
    {
        _droneMissileObjectPool = new GameObject[_missilePoolSize];
        
        for (int i = 0; i < _missilePoolSize; i++)
        {
            GameObject bullet = Instantiate(droneMissileFactory);
            bullet.SetActive(false);
            _droneMissileObjectPool[i] = bullet;
        }
    } 
    
    void FireDron()
    {
        for (int i = 0; i < _missilePoolSize; i++)
        {
            _droneMissile = _droneMissileObjectPool[i];
            if (_droneMissile.activeSelf == false)
            {
                
                _droneMissile.SetActive(true);
                _droneMissile.transform.position = droneMissilePosition.transform.position;
                break;
            }
        }
    }

    IEnumerator OnTriggerStay(Collider collision)
    {
        Debug.Log(GameDataManager.Instance.DroneLevel);
        switch (GameDataManager.Instance.DroneLevel)
        {
            case 1:
            {
                if (collision.CompareTag("Enemy") && _isajtlrl)
                {
                    _isajtlrl = false;
                    target = collision.gameObject.transform;
            
                    FireDron();
                    yield return new WaitForSeconds(_missileAttackDelay[0]);
                    _droneMissile.GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
                    _isajtlrl = true;
                }
                break;
            }
            case 2:
            {
                if (collision.CompareTag("Enemy") && _isajtlrl)
                {
                    _isajtlrl = false;
                    target = collision.gameObject.transform;
            
                    FireDron();
                    yield return new WaitForSeconds(_missileAttackDelay[1]);
                    _droneMissile.GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
                    _isajtlrl = true;
                }
                break;
            }
            case 3:
            {
                if (collision.CompareTag("Enemy") && _isajtlrl)
                {
                    _isajtlrl = false;
                    target = collision.gameObject.transform;
            
                    FireDron();
                    yield return new WaitForSeconds(_missileAttackDelay[2]);
                    _droneMissile.GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
                    _isajtlrl = true;
                }
                break;
            }
            case 4:
            {
                if (collision.CompareTag("Enemy") && _isajtlrl)
                {
                    _isajtlrl = false;
                    target = collision.gameObject.transform;
            
                    FireDron();
                    yield return new WaitForSeconds(_missileAttackDelay[3]);
                    _droneMissile.GetComponent<Rigidbody>().velocity = Vector3.up * (jumpSpeed * 1.75f);
                    _isajtlrl = true;
                }
                break;
            }
            case 5:
            {
                if (collision.CompareTag("Enemy") && _isajtlrl)
                {
                    _isajtlrl = false;
                    target = collision.gameObject.transform;

                    FireDron();
                    yield return new WaitForSeconds(_missileAttackDelay[4]);
                    _droneMissile.GetComponent<Rigidbody>().velocity = Vector3.up * (jumpSpeed * 3.0f);
                    _isajtlrl = true;
                }
                break;
            }
        }
    }
}
