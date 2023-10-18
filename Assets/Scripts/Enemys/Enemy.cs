using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private Vector3 _dir;
    public float speed = 5f;

    public float attackRange = 2f;
    public float attackDeley = 1f;

    private bool canAttack = true;
    
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        transform.LookAt(transform.position + _dir);
        _dir = target.transform.position - transform.position;
        _dir.Normalize();
        transform.position += _dir * (speed * Time.fixedDeltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
            canAttack = false;
            StartCoroutine(ResetAttackState());
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("공격");
        yield return new WaitForSeconds(attackDeley);
        // 실제 공격 코드
    }

    IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(attackDeley);
        canAttack = true;
    }
    
}
