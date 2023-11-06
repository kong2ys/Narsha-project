using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    
    private bool _isDamage = false;
    private bool _isWkdvksDamage = false;
    private float _duration = 5.0f;
    

    private void OnEnable()
    {
        _isDamage = false;
        _isWkdvksDamage = false;
        StartCoroutine(DestroyBomb());
    }

    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other) // 사거리 내로 적이 들어왔을 때
    {
        if (other.CompareTag("Enemy") && !_isDamage && GameDataManager.Instance.GrenadeDamage <= 4)
        {
            Debug.Log("폭탄에 맞음 ㅇㅇ");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage);
            }
        }
    }

    IEnumerator OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && GameDataManager.Instance.GrenadeDamage >= 4)
        {
            Debug.Log("갈!");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 10f);
        }
        else
        {
            if (other.CompareTag("Enemy") && !_isWkdvksDamage && GameDataManager.Instance.GrenadeDamage <= 4)
            {
                Debug.Log("장판에 지짐 ㅇㅇ");
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 0.05f);
                }
                _isWkdvksDamage = true;
            }
        
            yield return new WaitForSeconds(1.0f);
            _isWkdvksDamage = false;
        }
    }

    IEnumerator DestroyBomb() //생성후 잠시 대기
    {
        yield return new WaitForSeconds(0.2f);
        _isDamage = true;
        
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
    }
}
