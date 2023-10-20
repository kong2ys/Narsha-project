using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController :  MonoBehaviour
{
    public GameObject selectSkillWindow;
    
    private int _currentPlayerLevel;

    CharacterController _characterController;
    
    public float playerSpeed = 7.0f;
    public float gravity = -20.0f;
    private float _yVelocity = 0;
    
    public bool isDamaging = false;
    
    private Vector3 _dir;
    
    private GameObject _bullet;

    private int _poolSize = 50;
    private GameObject[] _bulletObjectPool;
    public GameObject bulletFactory;
    public GameObject[] firePosition; // 총알이 만들어질 위치

    public Camera followCamera;

    //public int fireLevel = 1;
    
    enum ActiveSkillList
    {
        Turret
    }

    private ActiveSkillList _haveActiveSkill1;

    void Start()
    {
        _currentPlayerLevel = GameDataManager.Instance.PlayerLevel;
        
        _characterController = GetComponent<CharacterController>();

        MakeBullet();
        StartCoroutine(Fire());
    }

    void Update()
    {
        Move();
        Turn();
        LevelUp();

        if (GameDataManager.Instance.PlayerLevel-1 == _currentPlayerLevel) // 레벨업 시 스킬 선택창 띄우기
        {
            selectSkillWindow.SetActive(true);
            _currentPlayerLevel = GameDataManager.Instance.PlayerLevel;
        }
    }
    
    void MakeBullet() // 시작 시, 총알 생성
    {
        _bulletObjectPool = new GameObject[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            _bulletObjectPool[i] = bullet;
        }
    }
    
    public void Turn() // 마우스 방향 회전
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

    void Move() // 이동
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _dir = new Vector3(h, 0, v);
        _dir = _dir.normalized;

        transform.position += _dir * (playerSpeed * Time.deltaTime);

        _yVelocity += gravity * Time.deltaTime;
        _dir.y = _yVelocity;
        _characterController.Move(_dir * (playerSpeed * Time.deltaTime));
    }
    
    void LevelUp() // 이걸로 유니티 인스펙터 창에서 힘들게 직접 바꿔가며 하지 말고 P눌러 1렙 올리고 M눌러 1렙 내리셈 ㅇㅇ 단순 테스트용임 ㅇㅇ
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameDataManager.Instance.PlayerLevel++;
            Debug.Log("플레이어렙" + GameDataManager.Instance.PlayerLevel);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameDataManager.Instance.FireLevel < 5)
            {
                GameDataManager.Instance.FireLevel += 1;
                Debug.Log("기본발사렙" + GameDataManager.Instance.FireLevel);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (GameDataManager.Instance.FireLevel > 0)
            {
                GameDataManager.Instance.FireLevel -= 1;
                Debug.Log("기본발사렙" + GameDataManager.Instance.FireLevel);
            }
        }
    }

    public void OnDamage(int damage) // 데미지 입기
    {
        GameDataManager.Instance.PlayerHp -= damage;
        Debug.Log("남은 HP : " + GameDataManager.Instance.PlayerHp);
    }

    IEnumerator Fire() // 기본 공격
    {
        switch (GameDataManager.Instance.FireLevel)
        {
            case 0:
            {
                GameDataManager.Instance.AttackDelay = 2.0f;
                
                for (int i = 0; i < _poolSize; i++)
                {
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
            case 1: // 기본 공격 레벨1
            {
                GameDataManager.Instance.AttackDelay = 1.0f;
                
                for (int i = 0; i < _poolSize; i++)
                {
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
            case 2: // 기본 공격 레벨2
            {
                GameDataManager.Instance.AttackDelay = 0.8f;
                
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
            case 3: // 기본 공격 레벨3
            {
                GameDataManager.Instance.AttackDelay = 0.65f;
                
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
            case 4: // 기본 공격 레벨4
            {
                GameDataManager.Instance.AttackDelay = 0.5f;
                
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
            case 5: // 기본 공격 레벨5
            { 
                for (int k = 0; k < 2; k++)
                {
                    GameDataManager.Instance.AttackDelay = 0.05f;
                    
                    for (int i = 0; i < _poolSize; i++)
                    {
                        _bullet = _bulletObjectPool[i];
                        if (_bullet.activeSelf == false)
                        {
                            _bullet.SetActive(true);
                            _bullet.transform.position = firePosition[2].transform.position;
                            break;
                        }
                    }
                }

                break;
            }
        }
        
        yield return new WaitForSeconds(GameDataManager.Instance.AttackDelay);

        StartCoroutine(Fire());
    }
}