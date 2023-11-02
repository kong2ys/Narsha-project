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

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        MakeArrow();
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

    public void Shoot()
    {
        switch (GameDataManager.Instance.ArrowLevel)
        {
            case 1:
            {
                for (int j = 0; j < _poolSize; j++)
                {
                    _arrow = _arrowObjectPool[j];
                    if (_arrow.activeSelf == false)
                    {
                        _arrow.SetActive(true);
                        _arrow.transform.position = arrowFirePosition[1].transform.position;
                        break;
                    }
                }

                break;
            }
            case 2:
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
            case 3:
            {
                for (int i = 0; i < 3; i++)
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
            case 4:
            {
                for (int i = 0; i < 4; i++)
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
    }
}
