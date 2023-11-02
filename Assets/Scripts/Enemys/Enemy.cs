using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpawnData spawnData;

    public void Initialize(SpawnData data)
    {
        spawnData = data;
    }
    
    public Rigidbody target;
    public float speed;
    public float health;
    public float maxHealth;

    public float attackRange = 2f;
    public float attackDeley = 1f;

    private bool canAttack = true;
    private bool isLive;
    
    private Rigidbody rigid;
    private WaitForFixedUpdate wait;
    private Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        wait = new WaitForFixedUpdate();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        maxHealth = spawnData.health;
        speed = spawnData.speed;
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
        
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.transform.position.z));
        
    }
    
    private void OnEnable() 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
        isLive = true;
        health = maxHealth;
    }
    
    public void Init(SpawnData data)
    {
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health > 0)
        {
            StartCoroutine(KnokBack());
        }
        else
        {
            Debug.Log("Die");
            Dead();
        }
    }

    IEnumerator KnokBack()
    {
        yield return wait; // 1 frame delay
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
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