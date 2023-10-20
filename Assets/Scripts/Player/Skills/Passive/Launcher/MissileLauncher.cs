using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_goMissile = null;//미사일 프리팹
    [SerializeField] Transform m_tfMissileSpawn = null;//미사일 발사 위치
    public float JumpSpeed = 5f;
    
    public Transform _Target = null;

    private GameObject[] Dron_bulletObjectPool;
    private int Dron_PoolSize = 10;
    private GameObject Dron_Bullet;
    
    public float MissileAttackDelay = 1f;

    private bool _isajtlrl = true;

    public float RotateSpeed = 5f;

    private Collider _collider;
    
    public Transform FloowTarget;
    public Vector3 offset;
    private void Start()
    {
        _collider = GetComponent<Collider>();
      MakeDronBullet();  
    }
    void MakeDronBullet()
    {
        Dron_bulletObjectPool = new GameObject[Dron_PoolSize];
        
        for (int i = 0; i < Dron_PoolSize; i++)
        {
            GameObject bullet = Instantiate(m_goMissile);
            bullet.SetActive(false);
            Dron_bulletObjectPool[i] = bullet;
        }
    }

    void FireDron()
    {
        for (int i = 0; i < Dron_PoolSize; i++)
        {
            Dron_Bullet = Dron_bulletObjectPool[i];
            if (Dron_Bullet.activeSelf == false)
            {
                
                Dron_Bullet.SetActive(true);
                Dron_Bullet.transform.position = m_tfMissileSpawn.transform.position;
                break;
            }
        }
    }

    IEnumerator OnTriggerStay(Collider collision)
    {
        
        if (collision.CompareTag("Enemy") && _isajtlrl)
        {
            _isajtlrl = false;
            _Target = collision.gameObject.transform;
            
            FireDron();
            yield return new WaitForSeconds(MissileAttackDelay);
            Dron_Bullet.GetComponent<Rigidbody>().velocity = Vector3.up * JumpSpeed;
            _isajtlrl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (FloowTarget.position + offset);
        transform.LookAt(_Target);
    }
}
