using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    private float _duration = 0.5f;

    private void OnEnable()
    {
        if (GameDataManager.Instance.GrenadeLevel >= 1)
        {
            _duration = 5f;
        }
        StartCoroutine(DestroyBomb());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("갈!");
                enemy.TakeDamage(GameDataManager.Instance.GrenadeDamage * 10f);
            }
        }
    }

    IEnumerator DestroyBomb() //생성후 잠시 대기
    {
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
    }
}
