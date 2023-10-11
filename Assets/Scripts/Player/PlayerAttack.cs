using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject _bullet;

    private int _poolSize = 50;
    private GameObject[] _bulletObjectPool;
    public GameObject bulletFactory;
    public GameObject[] firePosition; // 총알이 만들어질 위치

    public Camera followCamera;

    public float attackDelay = 1f;

    public int fireLevel = 1;

    void Start()
    {
        _bulletObjectPool = new GameObject[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            _bulletObjectPool[i] = bullet;
        }

        StartCoroutine(Fire());
    }

    void Update()
    {
        Turn();
        LevelUp();
    }

    void LevelUp() // 이걸로 유니티 인스펙터 창에서 힘들게 직접 바꿔가며 하지 말고 P눌러 1렙 올리고 M눌러 1렙 내리셈 ㅇㅇ 단순 테스트용임 ㅇㅇ
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (fireLevel < 5)
            {
                fireLevel += 1;
                Debug.Log(fireLevel);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (fireLevel > 1)
            {
                fireLevel -= 1;
                Debug.Log(fireLevel);
            }
        }
    }

    public void Turn()
    {
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit))
        {
            Vector3 nextvec = new Vector3(rayHit.point.x - transform.position.x, 0,
                rayHit.point.z - transform.position.z);
            transform.LookAt(transform.position + nextvec);
        }
    }

    IEnumerator Fire()
    {
        switch (fireLevel)
        {
            case 1: // 레벨1
            {
                for (int i = 0; i < _poolSize; i++)
                {
                    Debug.Log(i);
                    _bullet = _bulletObjectPool[i];
                    if (_bullet.activeSelf == false)
                    {
                        _bullet.SetActive(true);
                        _bullet.transform.position = firePosition[2].transform.position;
                        break;
                    }
                }

                break;
            }
            case 2: // 레벨2
            {
                for (int j = 1; j < 4; j += 2)
                {
                    for (int i = 0; i < _poolSize; i++)
                    {
                        _bullet = _bulletObjectPool[i];
                        if (_bullet.activeSelf == false)
                        {
                            _bullet.SetActive(true);
                            _bullet.transform.position = firePosition[j].transform.position;
                            break;
                        }
                    }
                }

                break;
            }
            case 3: // 레벨3
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int i = 0; i < _poolSize; i++)
                    {
                        _bullet = _bulletObjectPool[i];
                        if (_bullet.activeSelf == false)
                        {
                            _bullet.SetActive(true);
                            _bullet.transform.position = firePosition[j].transform.position;
                            _bullet.transform.rotation = firePosition[j].transform.rotation;
                            break;
                        }
                    }
                }

                break;
            }
            case 4: // 레벨4
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < _poolSize; i++)
                    {
                        _bullet = _bulletObjectPool[i];
                        if (_bullet.activeSelf == false)
                        {
                            _bullet.SetActive(true);
                            _bullet.transform.position = firePosition[j].transform.position;
                            _bullet.transform.rotation = firePosition[j].transform.rotation;
                            break;
                        }
                    }
                }

                break;
            }
            case 5: // 레벨5
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < _poolSize; i++)
                        {
                            _bullet = _bulletObjectPool[i];
                            if (_bullet.activeSelf == false)
                            {
                                _bullet.SetActive(true);
                                _bullet.transform.position = firePosition[j].transform.position;
                                _bullet.transform.rotation = firePosition[j].transform.rotation;
                                break;
                            }
                        }
                    }

                    yield return new WaitForSeconds(attackDelay / 5);
                }

                break;
            }
        }

        yield return new WaitForSeconds(attackDelay);

        StartCoroutine(Fire());
    }
}