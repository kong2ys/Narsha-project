using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player :  MonoBehaviour
{
    CharacterController _characterController;
    
    public float playerSpeed = 7.0f;
    public float gravity = -20.0f;
    private float _yVelocity = 0;
    
    public float playerHp;
    public bool isDamaging = false;
    
    private Vector3 _dir;

    public GameObject hitEffectFactory;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _dir = new Vector3(h, 0, v);
        _dir = _dir.normalized;

        transform.position += _dir * (playerSpeed * Time.deltaTime);

        _yVelocity += gravity * Time.deltaTime;
        _dir.y = _yVelocity;
        _characterController.Move(_dir * (playerSpeed * Time.deltaTime));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isDamaging)
        {
            Debug.Log("닿음");
            StartCoroutine(OnDamaged(25));
        }
    }

    IEnumerator OnDamaged(float damage)
    {
        isDamaging = true;
        playerHp -= damage;
        
        Debug.Log("플레이어 HP" + playerHp);

        yield return new WaitForSeconds(1.0f);
        isDamaging = false;
    }
}
