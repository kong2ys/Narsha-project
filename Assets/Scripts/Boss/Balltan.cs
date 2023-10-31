using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Random = UnityEngine.Random; // OnDrawGizmos

public class Balltan : MonoBehaviour
{
    private bool _move = true;
    public int pattern = 0;
    public float dash = 1.25f;
    public float currentTime;
    public float speed = 1f;

    public float pt0Time = 1.5f;
    public float pt1Time = 2f;
    public float pt2Time = 6f;
    public float pt3Time = 2f;
    public float pt4Time = 1.5f;

    public float hp = 1500;
    public float maxhp = 1500;

    private bool summonWall = false;
    private bool drawLine = false;

    private Vector3 _dir;
    
    public Transform targetPos;    // 부채꼴에 포함되는지 판별할 타겟
    public Transform lookTarget;    // 부채꼴에 포함되는지 판별할 타겟

    public float angleRange = 60f;
    public float radius = 8f;

    Color _red = new Color(1f, 0f, 0f, 0.2f);
    
    public GameObject target;
    public GameObject wall;
    private GameObject circle;
    public GameObject pt2Dis;
    public Slider hpslider;
    void Awake()
    {
        GameObject circlemap = Instantiate(wall);
        circlemap.SetActive(false);
        circle = circlemap;

    }

    // Update is called once per frame
    void Update()
    {
        hpslider.value = (float)hp / (float)maxhp;
        Pattern();
        Move();
    }
    private void OnDrawGizmos()
    {
        //부채꼴 그리기
        Handles.color = _red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
    
    void Pattern()
    {
        switch (pattern)
        {
            case 0: //추적 상태
                transform.LookAt(transform.position + _dir);
                _dir = lookTarget.transform.position - transform.position;
                _dir.Normalize();
                
                if (summonWall)
                {
                    if (currentTime == 0)
                        pt0Time = Random.Range(2, 4);
                    else if (currentTime > pt0Time)
                    {
                        currentTime = 0;
                        pattern = Random.Range(2, 5);
                        break;
                    }
                    currentTime += Time.deltaTime;
                }

                _move = true;
                break;
            
            case 1: //벽 생성
                if (currentTime == 0)
                    currentTime = pt1Time;
                var position = lookTarget.transform.position;
                transform.position = new Vector3(position.x + 5,position.y,position.z);
                _move = false;
                Wall();
                break;
            
            case 2: //돌진
                if (currentTime == 0)
                    currentTime = pt2Time;
                _move = false;
                Dash();
                break;
            case 3: //부채꼴 범위 공격
                if (currentTime == 0)
                    currentTime = pt3Time;
                _move = false;
                Pizza();
                break;
            case 4: //전방으로 이동하며 회전 공격
                if (currentTime == 0)
                    currentTime = pt4Time;
                _move = true;
                Spin();
                break;
                
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Wall")) //돌진 충돌 판정
        {
            Dash();
            drawLine = false;
            pattern = 0;
            currentTime = 0;
        }
        else if (collision.CompareTag("Player") && pattern == 2) //돌진 피해
        {
            target.GetComponent<BossScenePlayerController>().DamageAction(30);
        }
        else if (collision.CompareTag("Attack"))
        {
            BossScenePlayerController player = target.GetComponent<BossScenePlayerController>();
            hp -= player.attackPower;
        }
    }
    void Wall()
    {
 
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            circle.transform.position = transform.position;
            circle.SetActive(true); 
            summonWall = true; 
            pattern = 0; 
            currentTime = 0;
        }
        
    }
    void Dash()
    {
        currentTime -= Time.deltaTime; 
        _dir = transform.forward.normalized;
        if (pt2Time/5 * 2> currentTime)
        {
            transform.position += _dir * (dash * Time.deltaTime);
        }
        else if (pt2Time/5 * 2 < currentTime && pt2Time / 5 * 3 > currentTime)
        {
            if (!drawLine)
            {
                Instantiate(pt2Dis, transform.position, transform.rotation);
                drawLine = true;
            }
        }
        if (currentTime < 0)
        {
            pt2Dis.transform.position = transform.position;
            pattern = 0; 
            currentTime = 0;
            drawLine = false;
        }

        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Pizza()
    {
        currentTime -= Time.deltaTime;
        Vector3 interV = targetPos.position - transform.position; 
        // target과 나 사이의 거리가 radius 보다 작다면
        if (interV.magnitude <= radius)
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot); 
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree <= angleRange / 2f && currentTime < 0)
            {
                target.GetComponent<BossScenePlayerController>().DamageAction(130); 
            }
        }
            
        if (currentTime < 0)
        {
            pattern = 0;
            currentTime = 0;
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Spin()
    {
        currentTime -= Time.deltaTime;
        
        float pt3Dis = Vector3.Distance(target.transform.position, transform.position);
        if (pt3Dis < 5f && currentTime < 0.2f)
        {
            BossScenePlayerController player = target.GetComponent<BossScenePlayerController>();
            Debug.Log($"회전에 부딪힘 {GameDataManager.Instance.PlayerHp}");
            player.DamageAction(5);
            player.stopMove += 0.1f;
            target.transform.position += transform.forward * (Time.deltaTime * 10f);
        }
        if (currentTime < 0)
        {
            pattern = 0;
            currentTime = 0;
        }
    }

    void Move()
    {
        float moveDis = Vector3.Distance(target.transform.position, transform.position);
        if (_move && summonWall && moveDis > 5f)
            transform.position += transform.forward * (Time.deltaTime * speed);
    }
}
