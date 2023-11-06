using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterCall : MonoBehaviour
{
    public GameObject busterExplotion;
    public Transform ExplotionMakePosition;//만들어질 위치
    private GameObject _Explotion;//생성될 그거
    private GameObject[] _ExplotionObjectPool;//오브젝트 풀
    private int _ExplotionPoolSize = 3;//풀 사이즈
    
    public float _duration = 3.5f;


    
    private void Awake()
    {
        _ExplotionObjectPool = new GameObject[_ExplotionPoolSize];

        for (int i = 0; i < _ExplotionPoolSize; i++)
        {
            GameObject grenade = Instantiate(busterExplotion);
            grenade.SetActive(false);
            _ExplotionObjectPool[i] = grenade;
        }
    }


    

    void OnEnable()
    {
        StartCoroutine(BusterBomb());
    }

    IEnumerator BusterBomb() //생성후 잠시 대기
    {//실행 안됨
        yield return new WaitForSeconds(_duration);
        
        CreatExplotion();
        gameObject.SetActive(false);
    }

    void CreatExplotion()
    {
        for (int i = 0; i < _ExplotionPoolSize+1; i++) 
        {
            _Explotion = _ExplotionObjectPool[i];
            if (_Explotion.activeSelf == false)
            {
                _Explotion.transform.position = ExplotionMakePosition.position;
                _Explotion.SetActive(true);
                break;
            }
        }
    }
}
