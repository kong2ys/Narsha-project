using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceage : MonoBehaviour
{
    public float slow = 0.7f;
    private float _duration = 5.0f;
    private float _orgSpeed;

    private void OnEnable()
    {
        StartCoroutine(Duration());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            _orgSpeed = enemy.speed;
            enemy.speed *= slow;
            Debug.Log("됐냐?"+enemy.speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy1 = other.GetComponent<Enemy>();
        if (enemy1 != null)
        {
            enemy1.speed = _orgSpeed;
            Debug.Log("바꼈냐? " + enemy1.speed);
        }
    }


    IEnumerator Duration()
    {
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
    }
}
