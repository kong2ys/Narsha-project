using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWindDamage : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;//생성위치
    private void OnEnable()
    {
        target = GameObject.FindWithTag("Player");
        StartCoroutine(duration());
    }

    private void Update()
    { 
        transform.position = target.transform.position+offset;
    }

    IEnumerator duration()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}