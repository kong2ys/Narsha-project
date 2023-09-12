using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject HitFire;//프리팹
    public int HitDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Fire"))
        {
            GameObject explosion = Instantiate(HitFire);
            explosion.transform.position = transform.position;
            
        }
        

    }
}
