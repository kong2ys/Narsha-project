using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public GameObject bombFactory;
    public GameObject bombPosition;

    private GameObject[] _bombObjectPool;
    private int _bombPoolSize = 3;  
    private GameObject _bomb;

    public float bombDelay;
    private float _fallSpeed = 20.0f;

    void Awake()
    {
        _bombObjectPool = new GameObject[_bombPoolSize];
        
        for (int i = 0; i < _bombPoolSize; i++)
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.SetActive(false);
            _bombObjectPool[i] = bomb;
        }
    }

    void Update()
    {
        transform.position += new Vector3(0, -1, 0) * (_fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < _bombPoolSize; i++)
        {
            _bomb = _bombObjectPool[i];
            if (_bomb.activeSelf == false)
            {
                _bomb.SetActive(true);
                _bomb.transform.position = new Vector3(bombPosition.transform.position.x, 0, bombPosition.transform.position.z);
                break;
            }
        }
        gameObject.SetActive(false);
        
    }
}