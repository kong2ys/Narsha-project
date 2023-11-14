using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Missile : MonoBehaviour
{
    // private Rigidbody m_rigid = null;
    [SerializeField] private float missileSpeed = 50f; //맥스 속도
    [SerializeField] private float missileCurrentSpeed; // 날아가는 속도
    private Transform _missileTarget;

    public void SearchEnemy()
    {
        Drone drone = GameObject.Find("Drone").GetComponent<Drone>();
        _missileTarget = drone.target;
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
        _missileTarget = null;
        missileCurrentSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_missileTarget != null) //타겟있으면
        {
            if (missileCurrentSpeed <= missileSpeed) //최대 속도보다 느리면
            {
                missileCurrentSpeed += missileSpeed * Time.deltaTime; //점점 빨라짐
            }

            transform.position += transform.up * (missileCurrentSpeed * Time.deltaTime);
            Vector3 dir = (_missileTarget.position - transform.position).normalized; //타겟 방향
            transform.up = Vector3.Lerp(transform.up, dir, 0.25f); //돌격
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        
        if (enemy == null)
        {
            return;
        }
        
        if (enemy != null)
        {
            _missileTarget = null;
            enemy.TakeDamage(GameDataManager.Instance.FireDamage);
            Debug.Log("맞았다!");
            gameObject.SetActive(false);
        }
    }
}