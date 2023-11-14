using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateIceAge : MonoBehaviour
{
    public GameObject icsFactory;//프리팹
    public Transform iceMakePosition;//만들어질 위치
    private GameObject _ice;//생성될 그거
    private GameObject[] _iceObjectPool;//오브젝트 풀
    private int _icePoolSize = 1;//풀 사이즈

    private bool _isCoolTime = true;
    public float _coolTime = 15.0f;
    
    void Awake()
    {
        _iceObjectPool = new GameObject[_icePoolSize];
        for (int i = 0; i < _icePoolSize; i++)
        {
            GameObject iceAge = Instantiate(icsFactory);
            iceAge.SetActive(false);
            _iceObjectPool[i] = iceAge;
        }
    }

    void OnEnable()
    {
        StartCoroutine(CoolTime());
    }

    void MakeIceAge()
    {
        for (int i = 0; i < _icePoolSize; i++) 
        {
            _ice = _iceObjectPool[i];
            if (_ice.activeSelf == false)
            {
                _ice.transform.position = iceMakePosition.position;
                _ice.SetActive(true);
                break;
            }
        }
    }

    IEnumerator CoolTime()
    {
        MakeIceAge();
        yield return new WaitForSeconds(_coolTime);
        CoolTime();
    }
}