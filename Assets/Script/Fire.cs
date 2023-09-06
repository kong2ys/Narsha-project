using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    
    public float speed = 5.0f;
    private Vector3 dir;
    public float maxDistance ;
    private Vector3 startPos;
   

    // Start is called before the first frame update


    void OnEnable()
    {
        GameObject target = GameObject.Find("Player");
        transform.rotation = target.transform.rotation;
        startPos = target.transform.position; 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, startPos) > maxDistance)
        {
            gameObject.SetActive(false);
        }
        Shoot();
    }

    void Shoot()
    {
        dir = transform.forward.normalized;
        transform.position +=  dir * (speed * Time.deltaTime);
    }
    
}
