using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Missile : MonoBehaviour
{
    private Rigidbody m_rigid = null;
    [SerializeField] private float m_speed = 0f; //맥스 속도
    private float m_currentSpeed; // 날아가는 속도
    private Transform m_tfTarget;


    public void SearchEnemy()
    {
        MissileLauncher missileLauncher = GameObject.Find("Launcher").GetComponent<MissileLauncher>();
        m_tfTarget = missileLauncher._Target;
    }

    IEnumerator LauncherDelay() //생성후 잠시 대기
    {
        // yield return new WaitUntil(()=>m_rigid.velocity.y < 0f);
        yield return new WaitForSeconds(0.1f);
        SearchEnemy();

        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        Debug.Log("nigger activated");
    }

    private void OnEnable()
    {
        StartCoroutine(LauncherDelay());
    }

    private void OnDisable()
    {
        m_tfTarget = null;
        m_currentSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_tfTarget != null) //타겟있으면
        {
            if (m_currentSpeed <= m_speed) //최대 속도보다 느리면
            {
                m_currentSpeed += m_speed * Time.deltaTime; //점점 빨라짐
            }

            transform.position += transform.up * m_currentSpeed * Time.deltaTime;
            Vector3 dir = (m_tfTarget.position - transform.position).normalized; //타겟 방향
            transform.up = Vector3.Lerp(transform.up, dir, 0.25f); //돌격
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            m_tfTarget = null;
            gameObject.SetActive(false);
        }
    }
}