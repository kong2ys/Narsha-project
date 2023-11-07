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
    public float throwPower = 3f;
    
    
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
        
        StartCoroutine(DamagSurutan());
    }
    IEnumerator DamagSurutan() //생성후 잠시 대기
    {
        yield return new WaitForSeconds(3f);
        gal();
        gameObject.SetActive(false);
    }

    
}
