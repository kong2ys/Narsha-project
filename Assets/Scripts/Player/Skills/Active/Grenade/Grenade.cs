using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject bombFactory;
    public GameObject bombPosition;

    private GameObject[] _bombObjectPool;
    private int _bombPoolSize = 3;  
    private GameObject _bomb;

    public float bombDelay;
    private float _fallSpeed = 20.0f;

    private void Start()
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
        if (other.CompareTag("Floor"))
        {
            for (int i = 0; i < _bombPoolSize; i++)
            {
                _bomb = _bombObjectPool[i];
                if (_bomb.activeSelf == false)
                {
                    _bomb.SetActive(true);
                    _bomb.transform.position = bombPosition.transform.position;
                    break;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
