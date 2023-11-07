using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrenade : MonoBehaviour
{
    public GameObject grenadeFactory;
    public Transform grenadeMakePosition;
    public Vector3 offset;

    private GameObject _grenade;
    private GameObject[] _grenadeObjectPool;
    private int _grenadePoolSize = 3;

    private bool _isCoolTime = false;
    private float _coolTime = 1.0f;

    public GameObject busterFactory;//프리팹
    public Transform busterMakePosition;//만들어질 위치
    private GameObject _buster;//생성될 그거
    private GameObject[] _busterObjectPool;//오브젝트 풀
    private int _busterPoolSize = 3;//풀 사이즈
    
    public GameObject surutanFactory;//프리팹
    private GameObject _surutan;//생성될 그거
    private GameObject[] _surutanObjectPool;//오브젝트 풀
    private int _surutanPoolSize = 3;//풀 사이즈

    void Start()
    {
        _grenadeObjectPool = new GameObject[_grenadePoolSize];
        
        for (int i = 0; i < _grenadePoolSize; i++)
        {
            GameObject grenade = Instantiate(grenadeFactory);
            grenade.SetActive(false);
            _grenadeObjectPool[i] = grenade;
        }

        _busterObjectPool = new GameObject[_busterPoolSize];
        for (int i = 0; i < _busterPoolSize; i++)
        {
            GameObject buster = Instantiate(busterFactory);
            buster.SetActive(false);
            _busterObjectPool[i] = buster;
        }

        _surutanObjectPool = new GameObject[_surutanPoolSize];
        for (int i = 0; i < _surutanPoolSize; i++)
        {
            GameObject surutan = Instantiate(surutanFactory);
            surutan.SetActive(false);
            _surutanObjectPool[i] = surutan;
        }
    }
    
    void Update()
    {
        _grenadePoolSize = GameDataManager.Instance.GrenadeLevel;
        _busterPoolSize = GameDataManager.Instance.GrenadeLevel;
        
        if (Input.GetKeyDown(KeyCode.E) && !_isCoolTime)
        {
            MakeGrenade();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !_isCoolTime)
        {
            MakeBuster();
        }

        void MakeBuster()
        {
            for (int i = 0; i < _grenadePoolSize+1; i++) 
            {
                _grenade = _grenadeObjectPool[i];
                if (_grenade.activeSelf == false)
                {
                    _grenade.transform.position = grenadeMakePosition.position + offset;
                    _grenade.SetActive(true);
                    break;
                }
            }
        }


    void MakeGrenade()
    {
        _isCoolTime = true;
        switch (GameDataManager.Instance.GrenadeLevel)
        {
            case 0:
                
                for (int i = 0; i < _surutanPoolSize + 1; i++) 
                {
                    _surutan = _surutanObjectPool[i];
                    if (_surutan.activeSelf == false)
                    {
                        _surutan.transform.position = busterMakePosition.position;
                        Rigidbody rb = _surutan.GetComponent<Rigidbody>();
                        rb.AddForce(busterMakePosition.transform.forward * 5f, ForceMode.Impulse);
                        _surutan.SetActive(true);
                        break;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < _busterPoolSize+1; i++) 
                {
                    _buster = _busterObjectPool[i];
                    if (_buster.activeSelf == false)
                    {
                        _buster.transform.position = busterMakePosition.position;
                        _buster.SetActive(true);
                        break;
                    }
                }
                break;
        }

            StartCoroutine(CoolTime());
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(_coolTime);
        Debug.Log("폭탄 쿨 돔 ㅇㅇ");
        _isCoolTime = false;
    }
}
