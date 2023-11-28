using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWind : MonoBehaviour
{
    // 위치 기억 시간 간격
    public Transform target;
    public float rememberInterval = 3f;
    public float moveDuration = 0.05f; // 이동 시간
    private Vector3 rememberedPosition;
    public GameObject timeFactory;
    private GameObject time;
    private float rememberPlayerHp;
    private float HpLimit;
    
    private int[] coolTime = {60, 50, 40, 30, 20};
    private bool isReady = true;


    void OnEnable()
    {
        HpLimit = GameDataManager.Instance.PlayerHp;
        time = Instantiate(timeFactory);
        time.SetActive(false);
        StartCoroutine(PositionRemember());
        
    }
    
    private void Update()
    {
        time.transform.position = target.transform.position;
        if (GameDataManager.Instance.PlayerHp < HpLimit *0.3 && isReady)
        {
            Debug.Log(HpLimit);
            isReady = false;
            StartCoroutine(StartCoolTime(coolTime[GameDataManager.Instance.ReWindLevel - 1]));
            StartCoroutine(MoveToRememberedPosition());
        }
    }

    IEnumerator PositionRemember()
    {
        rememberPlayerHp = GameDataManager.Instance.PlayerHp;
        rememberedPosition = target.position;
        yield return new WaitForSeconds(rememberInterval);
        StartCoroutine(PositionRemember());
    }

    IEnumerator StartCoolTime(int CoolTime)
    {
        yield return new WaitForSeconds(CoolTime);
        isReady = true;
    }

    IEnumerator MoveToRememberedPosition()
    {
       
        time.SetActive(true); 
        time.transform.position = target.transform.position;
        Vector3 startPosition = target.position;
        float elapsedTime = 0f;

        while (true)
        {

            GameDataManager.Instance.PlayerHp = rememberPlayerHp;

            float t = elapsedTime / moveDuration;
            target.position = Vector3.Lerp(startPosition, rememberedPosition, t);
            elapsedTime += Time.deltaTime;

            // 카메라가 목표 위치에 도달했는지 확인
            if (target.position == rememberedPosition)
            {
                Debug.Log("무야호!");
                rememberedPosition = target.position;
                time.SetActive(false);
                break;
            } 
        

            yield return null;
        }
    }
}