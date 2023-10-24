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
    
    IEnumerator OnTriggerEnter(Collider collision) // 사거리 내로 적이 들어왔을 때
    {
        if (collision.CompareTag("Enemy") && !_isDamage)
        {
            Debug.Log("폭탄 맞음 ㅇㅇ");
            _isDamage = true;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            _isDamage = true;
        }
    }

    IEnumerator OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !_isWkdvksDamage)
        {
            Debug.Log("장판에 지짐 ㅇㅇ");
            _isWkdvksDamage = true;
        }

        yield return new WaitForSeconds(1.0f);
        _isWkdvksDamage = false;
    }

    IEnumerator DestroyBomb() //생성후 잠시 대기
    {
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
    }
}
