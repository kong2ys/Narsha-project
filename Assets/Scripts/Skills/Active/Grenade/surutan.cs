using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surutan : MonoBehaviour
{
    public GameObject surutanFactory;
    public GameObject sututanPosition;
    public Vector3 offset;
    
    private GameObject[] _surutanObjectPool;
    private int _surutanPoolSize = 3;  
    private GameObject _surutan;


    public GameObject ball; // 오브젝트 (공 또는 다른 오브젝트)
    public float force = 10.0f; // 가할 힘의 크기
    
    
    public GameObject madePosition;
    void Start()
    {
        
        _surutanObjectPool = new GameObject[_surutanPoolSize];
        
        for (int i = 0; i < _surutanPoolSize; i++)
        {
            GameObject surutan = Instantiate(surutanFactory);
            surutan.SetActive(false);
            _surutanObjectPool[i] = surutan;
        }
    }
    void gal()
    {
        for (int i = 0; i < _surutanPoolSize; i++)
        {
            _surutan = _surutanObjectPool[i];
            if (_surutan.activeSelf == false)
            {
                _surutan.SetActive(true);
                _surutan.transform.position = new Vector3(sututanPosition.transform.position.x, 0, sututanPosition.transform.position.z);
                break;
            }
        }
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if (ball != null)
        {
            // 오브젝트에 힘을 가하기 위해 위치를 이동합니다.
            Vector3 forceDirection = transform.forward; // 앞 방향으로 힘을 가하려면 transform.forward를 사용합니다.
            Vector3 thowfower = forceDirection * force;
            ball.transform.position += thowfower * Time.deltaTime;
        }
        
        StartCoroutine(DamageSurutan());
    }
    IEnumerator DamageSurutan() //생성후 잠시 대기
    {
        yield return new WaitForSeconds(3f);
        gal();
        gameObject.SetActive(false);
    }

    
}
