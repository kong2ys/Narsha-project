using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balltan : MonoBehaviour
{
    public int pattern = 0;
    public float dash = 1.25f;
    public float currentTime;
    public float pt1Time = 300.0f;
    public float pt2Time = 800.0f;

    private Vector3 dir;
    
    public GameObject target;
    private WaitForSeconds Seconds = new WaitForSeconds(0.01f);
    public GameObject wall;
    private GameObject circle;
    public GameObject pt2Dis;
    void Awake()
    {
        GameObject circlemap = Instantiate(wall);
        circlemap.SetActive(false);
        circle = circlemap;
        pt2Dis.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Pattern();
    }

    void Pattern()
    {
        switch (pattern)
        {
            case 0:
                transform.LookAt(target.transform);
                break;
            
            case 1:
                if (currentTime == 0)
                {
                    currentTime = pt1Time;
                }
                transform.position = target.transform.position;
                StartCoroutine(Wall());
                break;
            
            case 2:
                if (currentTime == 0)
                {
                    currentTime = pt2Time;
                }
                StartCoroutine(Dash());
                break;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("1231231232131");
            StopCoroutine(Dash());
            pt2Dis.transform.position = transform.position;
            pattern = 0;
            currentTime = 0;
        }
    }
    IEnumerator Wall()
    {
        while (currentTime > 0)
        {
            currentTime -= 0.1f;
            yield return Seconds;
        }

        circle.transform.position = transform.position;
        circle.SetActive(true);
        pattern = 0;
        currentTime = 0;
    }
    IEnumerator Dash()
    {
        while (currentTime > 0)
        {
            currentTime -= 0.1f;
            dir = transform.forward.normalized;
            if (pt2Time/5 * 3 > currentTime)
            {
                pt2Dis.SetActive(false);
                transform.position += dir * (dash * Time.deltaTime);
            }
            if (pt2Time/5 * 3.5 < currentTime && pt2Time/5 * 4 > currentTime)
            {
                Debug.Log("true");
                pt2Dis.SetActive(true);
                pt2Dis.transform.position += dir * (dash * 3 * Time.deltaTime);
            }
            yield return Seconds;
        }

        pattern = 0;
        currentTime = 0;
    }
}
