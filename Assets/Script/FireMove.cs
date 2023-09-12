using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireMove: MonoBehaviour
{
    
    public float speed = 5.0f;
    private Vector3 dir;
    public float maxDistance ;
    private Vector3 startPos;

    public void Awake()
    {
        
    }

    void OnEnable()
    {
        GameObject target = GameObject.Find("FirePosition");
        transform.rotation = target.transform.rotation;
        startPos = target.transform.position; 
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, startPos) > maxDistance)
        {
            gameObject.SetActive(false);
        }
        Shoot();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            gameObject.SetActive(false);
    }

    void Shoot()
    { 
        dir = transform.forward.normalized;
        transform.position +=  dir * (speed * Time.deltaTime);
    }

   
    
}
