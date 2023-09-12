using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFire : MonoBehaviour
{
    public int poolSize = 25;
    GameObject[]  bulletObjectPool;
    public GameObject bulletFactory;//프리팹

    public GameObject firePosition; //만들어질 위치
    public float attackDelay;

    private bool _isFire;
    public int amountOfBullets = 3;
    public GameObject[] bulletPositionList;

    //public GameObject skill;

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
        //skill YearSkill = skill.GetComponent<skill>();
        //bool skillOnee = YearSkill.SkillOne;
    }


    IEnumerator Fire()
    {
        for (int j = 0; j < amountOfBullets; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
               
                GameObject bullet = bulletObjectPool[i];
                if (bullet.activeSelf == false)
                {
                    bullet.SetActive(true);
                    bullet.transform.position = firePosition.transform.position;
                    GameObject target = GameObject.Find("FirePosition");
                    transform.rotation = target.transform.rotation;
                    
                    break;
                }
            }
        }

        yield return new WaitForSeconds(attackDelay);
        
        StartCoroutine(Fire());
    }
}