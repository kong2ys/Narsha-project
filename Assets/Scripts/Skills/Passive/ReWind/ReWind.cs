using System.Collections;
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
    public float coolTime = 60f;
    public float coolTimeLimit = 60f;
    public float HpLimit = 50;
    
    
    void Start()
    {
        time = Instantiate(timeFactory);
        time.SetActive(false);
        StartCoroutine(PositionRemember());
    }

    private void Update()
    {
        coolTime += Time.deltaTime;
        if (GameDataManager.Instance.PlayerHp <= HpLimit && coolTime >=coolTimeLimit || Input.GetKey(KeyCode.G))
        {
            coolTime = 0;
            StartCoroutine(MoveToRememberedPosition());
        }
    }

    IEnumerator PositionRemember()
    {
        while (true)
        {
            rememberPlayerHp = GameDataManager.Instance.PlayerHp;
            rememberedPosition = target.position;
            yield return new WaitForSeconds(rememberInterval);
        }
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
                
                
                break;
            } 
        

            yield return null;
        }
    }
}