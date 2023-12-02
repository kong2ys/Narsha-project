using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatTurret : MonoBehaviour
{
    private GameObject _turret;
    private int _turretPoolSize = 5;
    private GameObject[] _turretObjectPool;
    public GameObject turretFactory;
    public GameObject turretMakePosition;

    public float duration = 5.0f;
    public float coolTime = 7.0f;
    public bool isCoolTime = false;
    public float currentCoolTime;
    public Text coolTimeText;

    void Start()
    {
        currentCoolTime = coolTime;
        MakeTurret();
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCoolTime)
        {
            for (int i = 0; i < GameDataManager.Instance.TurretLevel; i++)
            {
                _turret = _turretObjectPool[i];
                if (_turret.activeSelf == false)
                {
                    coolTimeText.color = Color.white;
                    _turret.SetActive(true);
                    _turret.transform.position = turretMakePosition.transform.position;
                    isCoolTime = true;
                    currentCoolTime = coolTime;
                    StartCoroutine(TimeOverDisable(_turret));
                    break;
                }
            }
        }

        if (isCoolTime)
        {
            currentCoolTime -= Time.deltaTime;
            coolTimeText.text = String.Format("{0:F0}", currentCoolTime);
            if (currentCoolTime <= 0)
            {
                coolTimeText.text = String.Format("OK!");
                isCoolTime = false;
            }
        }
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

    IEnumerator TimeOverDisable(GameObject turret)
    {
        yield return new WaitForSeconds(duration);
        turret.SetActive(false);
    }
}
