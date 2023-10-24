using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private GameObject[] _bombObjectPool;
    public GameObject bombFactory;
    private int _bombPoolSize = 10;
    private GameObject _bomb;
    
    public GameObject bombPosition;

    public float bombDelay;

    private void Start()
    {
        _bombObjectPool = new GameObject[_bombPoolSize];
        
        for (int i = 0; i < _bombPoolSize; i++)
        {
            GameObject bullet = Instantiate(bombFactory);
            bullet.SetActive(false);
            _bombObjectPool[i] = bullet;
        }
    }
    
    private void OnEnable()
    {
        StartCoroutine(Bomb()); // Bomb 함수를 코루틴으로 실행
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(bombDelay);
        Explotion();
    }
    
    void Explotion()
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
