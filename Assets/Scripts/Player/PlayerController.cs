using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController :  MonoBehaviour
{
    public GameObject selectSkillWindow;
    
    private int _currentPlayerLevel;

    CharacterController _characterController;
    public Animator anim;
    
    public float gravity = -20.0f;
    private float _yVelocity = 0;
    private float _h;
    private float _v;

    private Vector3 _dir;
    
    private GameObject _bullet;

    private int _poolSize = 50;
    private GameObject[] _bulletObjectPool;
    public GameObject bulletFactory;
    public GameObject[] firePosition; // 총알이 만들어질 위치

    public Image[] activeSkill;
    public Image[] passiveSkill;
    
    private Camera _camera;

    public ArrowFire arrowFire;

    void Start()
    {
        GameDataManager.Instance.PlayerHp = GameDataManager.Instance.PlayerMaxHp;
        
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        GameDataManager.Instance.PlayerLevel = 1;
        GameDataManager.Instance.FireLevel = 0;
        GameDataManager.Instance.TurretLevel = 0;
        GameDataManager.Instance.GrenadeLevel = 0;
        GameDataManager.Instance.FireBallLevel = 0;
        GameDataManager.Instance.DroneLevel = 0;
        GameDataManager.Instance.ArrowLevel = 0;
        GameDataManager.Instance.IceLevel = 0;
        GameDataManager.Instance.CurrentExp = 0;
        GameDataManager.Instance.MaxExp = 10000;
        
        _currentPlayerLevel = GameDataManager.Instance.PlayerLevel;

        MakeBullet();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (GameDataManager.Instance.PlayerHp <= 0)
        {
            DieAction();
        }

        Move();
        Turn();
        LevelUp();

        if (GameDataManager.Instance.PlayerLevel-1 == _currentPlayerLevel) // 레벨업 시 스킬 선택창 띄우기
        {
            switch (GameDataManager.Instance.PlayerLevel)
            {
                case 5 or 10 or 15 or 20 or 30:
                {
                    GameDataManager.Instance.GrenadeLevel++;
                    activeSkill[0].color = new Color(255, 255, 255); 
                    break;
                }
                case 10 or 20 or 30 or 40 or 50:
                {
                    GameDataManager.Instance.TurretLevel++;
                    activeSkill[1].color = new Color(255, 255, 255); 
                    break;
                }
            }
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
    
    void Turn() // 마우스 방향 회전
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.transform.position.y; // Y축 위치를 카메라의 Y 위치로 설정
        Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);

        Vector3 nextVec = new Vector3(worldPos.x - transform.position.x, 0, worldPos.z - transform.position.z);
        transform.LookAt(transform.position + nextVec);
    }

    void Move() // 이동
    {
        if (_h == 0f && _v == 0f) // 애니메이션
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
        
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = _dir.normalized;

        _yVelocity += gravity * Time.deltaTime;
        _dir.y = _yVelocity;
        
        float totalSpeed = GameDataManager.Instance.PlayerMoveSpeed + GameDataManager.Instance.PlusDex;
        _characterController.Move(_dir * (totalSpeed * Time.deltaTime));
    }
    
    void LevelUp()
    {
        if (GameDataManager.Instance.CurrentExp >= GameDataManager.Instance.MaxExp)
        {
            GameDataManager.Instance.CurrentExp -= GameDataManager.Instance.MaxExp;
            GameDataManager.Instance.PlayerLevel++;
            GameDataManager.Instance.MaxExp *= 1.1f;
        }
    }

    public void DamageAction(float damage) // 데미지 입기
    {
        GameDataManager.Instance.PlayerHp -= damage;
        Debug.Log("남은 HP : " + GameDataManager.Instance.PlayerHp);
    }

    void DieAction()
    {
        Debug.Log("죽었다!!");
    }
    
    IEnumerator Fire() // 기본 공격
    {
        switch (GameDataManager.Instance.FireLevel)
        {
            case 0:
            {
                GameDataManager.Instance.AttackDelay = 1.75f;
                
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

        if (GameDataManager.Instance.ArrowLevel >= 1)
        {
            arrowFire.Shoot();
        }
        
        yield return new WaitForSeconds(GameDataManager.Instance.AttackDelay);

        StartCoroutine(Fire());
    }
}