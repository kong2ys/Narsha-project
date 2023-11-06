using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    private bool _isDamage = false;
    private float _duration = 5.0f;
    private int _index = 0;

    private void OnEnable()
    {
        _isDamage = false;
        StartCoroutine(DestroyBomb());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && GameDataManager.Instance.GrenadeLevel > 4)
        {
            Debug.Log("갈!");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 10f);
        }
        else
        {
            if (other.CompareTag("Enemy") && !_isDamage)
            {
                Debug.Log(34124324241324);
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (_index == 0)
                    {
                        Debug.Log("폭탄에 맞음");
                        enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 10f);
                    }
                    else
                    {
                        Debug.Log("장판에 지짐 ㅇㅇ");
                        enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 0.5f);
                    }
                }
                _isDamage = true;
            }
        }
    }

    IEnumerator DestroyBomb() //생성후 잠시 대기
    {
        for (_index = 0; _index < 5; _index++)
        {
            yield return new WaitForSeconds(_duration/5);
            _isDamage = false;
            Debug.Log("바뀜");
        }
        gameObject.SetActive(false);
    }
}
