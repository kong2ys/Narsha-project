using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{
    private GameObject _target;
    
    private int _poolSize = 50;
    private GameObject[] _arrowObjectPool;
    public GameObject arrowFactory;
    public GameObject[] arrowFirePosition;
    private GameObject _arrow;

    private float _attackDelay = 1.0f;
    
    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        MakeArrow();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        transform.position = _target.transform.position;
        transform.rotation = _target.transform.rotation;
    }
    
    void MakeArrow()
    {
        _arrowObjectPool = new GameObject[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowFactory);
            arrow.SetActive(false);
            _arrowObjectPool[i] = arrow;
        }
    }

    IEnumerator Shoot()
    {
        switch (GameDataManager.Instance.ArrowLevel)
        {
            case 1:
            {
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 0; j < _poolSize; j++)
                    {
                        _arrow = _arrowObjectPool[j];
                        if (_arrow.activeSelf == false)
                        {
                            _arrow.SetActive(true);
                            _arrow.transform.position = arrowFirePosition[i].transform.position;
                            break;
                        }
                    }
                }

                break;
            }
        }
        
        yield return new WaitForSeconds(_attackDelay);
        StartCoroutine(Shoot());
    }
}
