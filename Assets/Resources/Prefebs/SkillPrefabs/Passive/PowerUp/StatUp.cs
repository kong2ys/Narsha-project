using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatUp : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = target.position;
    }
    
    public enum StatType
    {
        Str,
        Hp,
        Dex
    };
    public StatType type;

    void OnEnable()
    {
        switch (type)
        {
            case StatType.Str:
            {
                GameDataManager.Instance.PlusStr += 0.5f;
                Debug.Log("공격력" + GameDataManager.Instance.PlusStr);
                break;
            }
            case StatType.Hp:
            {
                GameDataManager.Instance.PlayerHp *= 0.2f;
                Debug.Log("체력" + GameDataManager.Instance.PlayerHp);
                break;
            }
            case StatType.Dex:
            {
                GameDataManager.Instance.PlusDex += 0.5f;
                Debug.Log("이동속도" + GameDataManager.Instance.PlusDex);
                break;
            }
        }

        StartCoroutine(ShowEffect());
    }

    IEnumerator ShowEffect()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
