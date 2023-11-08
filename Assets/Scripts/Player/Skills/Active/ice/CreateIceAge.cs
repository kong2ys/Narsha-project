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
    
    // Start is called before the first frame update
    void Start()
    {
        
        _iceObjectPool = new GameObject[_icePoolSize];
        for (int i = 0; i < _icePoolSize; i++)
        {
            GameObject iceAge = Instantiate(icsFactory);
            iceAge.SetActive(false);
            _iceObjectPool[i] = iceAge;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(cooltime());
    }


    // Update is called once per frame
    void Update()
    {
        // 이 부분을 제거합니다.
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

    IEnumerator cooltime()
    {
        while(true)
        {
            yield return new WaitForSeconds(_coolTime);
            MakeIceAge();
        }
    }
}