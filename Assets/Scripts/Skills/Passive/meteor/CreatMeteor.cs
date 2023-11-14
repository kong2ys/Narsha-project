using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMeteor : MonoBehaviour
{
    
    private bool _isCoolTime = false;
    private float _coolTime = 1.0f;
    public Transform meteorMakePosition;//생성위치
    public Vector3 offset;//생성위치

    public GameObject meteorFactory;//프리팹
    private GameObject _meteor;//생성될 그거
    private GameObject[] _meteorObjectPool;//오브젝트 풀
    private int _meteorPoolSize = 3;//풀 사이즈
    
    public float _duration = 5f;
    void Awake()
    {
        _meteorObjectPool = new GameObject[_meteorPoolSize];
        
        for (int i = 0; i < _meteorPoolSize; i++)
        {
            GameObject meteor = Instantiate(meteorFactory);
            meteor.SetActive(false);
            _meteorObjectPool[i] = meteor;
        }
    }
    void Update()
    {
        _meteorPoolSize = GameDataManager.Instance.meteorLevel;
       
    }

    private void OnEnable()
    {
        StartCoroutine(CoolTime());
    }
    

    void Makemeteor()
    {
        for (int i = 0; i < _meteorPoolSize+1; i++) 
        {
            _meteor = _meteorObjectPool[i];
            if (_meteor.activeSelf == false)
            {
                _meteor.transform.position = meteorMakePosition.position + offset;
                _meteor.SetActive(true);
                break;
            }
        }
     
    }
    IEnumerator CoolTime()
    {
        while (true)
        {
            Makemeteor();
            yield return new WaitForSeconds(_coolTime+_duration);
        }
    }
}