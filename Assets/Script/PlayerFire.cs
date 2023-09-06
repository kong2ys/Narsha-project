using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    int poolSize = 25;
    GameObject[]  bulletObjectPool;
    public GameObject bulletFactory;//프리팹
    public GameObject firePosition;//만들어질 위치
    public float attackDelay;

    private bool _isFire;
    // Start is called before the first frame update
    void Start()
    {
        bulletObjectPool = new GameObject[poolSize];
        for (int i = 0; i< poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletObjectPool[i] = bullet;//활성 비활성화가 핵심
        }
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        //프리팹을 이용해서 빈오브젝트에 반영한다.
            //오브젝트 생성되면 이동한다.
    }


    IEnumerator Fire()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Debug.Log(i);
            GameObject bullet = bulletObjectPool[i];
            if (bullet.activeSelf == false)
            {
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
                break;
            }
        }

        yield return new WaitForSeconds(attackDelay);
        
        StartCoroutine(Fire());
    }
}