using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatTurret : MonoBehaviour
{
    private GameObject _turret;
    private int _turretPoolSize = 5;
    private GameObject[] _turretObjectPool;
    public GameObject turretFactory;
    public GameObject turretMakePosition;

    public float duration = 5.0f;
    
    void Start()
    {
        MakeTurret();
    }
    
    void Update()
    {
        InstallationTurret();
    }
    
    void MakeTurret() // 시작 시, 터렛 생성
    {
        _turretObjectPool = new GameObject[_turretPoolSize];
        for (int i = 0; i < _turretPoolSize; i++)
        {
            GameObject turret = Instantiate(turretFactory);
            turret.SetActive(false);
            _turretObjectPool[i] = turret;
        }
    }
    
    void InstallationTurret()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            for (int i = 0; i < GameDataManager.Instance.TurretLevel; i++)
            {
                _turret = _turretObjectPool[i];
                if (_turret.activeSelf == false)
                {
                    _turret.SetActive(true);
                    _turret.transform.position = turretMakePosition.transform.position;
                    StartCoroutine(TimeOverDisable(_turret));
                    break;
                }
            }
        }
    }

    IEnumerator TimeOverDisable(GameObject turret)
    {
        yield return new WaitForSeconds(duration);
        turret.SetActive(false);
    }
}
