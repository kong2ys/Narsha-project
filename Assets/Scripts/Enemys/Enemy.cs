using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody target;
    public float speed = 5f;

    public float attackRange = 2f;
    public float attackDeley = 1f;

    private bool canAttack = true;
    private bool isLive = true;
    
    private Rigidbody rigid;

    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (!isLive)
            return;
        
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;
        
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
            canAttack = false;
            StartCoroutine(ResetAttackState());
        }
    }
    
    private void OnEnable() 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
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