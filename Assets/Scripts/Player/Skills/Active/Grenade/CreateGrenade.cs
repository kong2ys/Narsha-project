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
    private float _coolTime = 10.0f;

    void Start()
    {
        _grenadeObjectPool = new GameObject[_grenadePoolSize];
        
        for (int i = 0; i < _grenadePoolSize; i++)
        {
            GameObject grenade = Instantiate(grenadeFactory);
            grenade.SetActive(false);
            _grenadeObjectPool[i] = grenade;
        }
    }
    
    void Update()
    {
        _grenadePoolSize = GameDataManager.Instance.GrenadeLevel;
        
        StartCoroutine(MakeGrenade());
    }

    IEnumerator MakeGrenade()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isCoolTime)
        {
            _isCoolTime = true;
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

        yield return new WaitForSeconds(_coolTime);
        _isCoolTime = false;
    }
}
