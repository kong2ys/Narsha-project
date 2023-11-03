using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PassiveAxe : MonoBehaviour
{
    public Transform target;
    public GameObject[] axes;

    public float radius;
    public float rotateSpeed;

    private float _deg;

    private void Update()
    {
        LevelUp();
        
        switch (GameDataManager.Instance.AxeLevel)
        {
            case 1:
            {
                AxesRotate(1);
                break;
            }
            case 2:
            {
                AxesRotate(2);
                break;
            }
            case 3:
            {
                AxesRotate(3);
                break;
            }
            case 4:
            {
                AxesRotate(4);
                break;
            }
            case 5:
            {
                AxesRotate(5);
                break;
            }
        }
    }
    
    void LevelUp() // 이걸로 유니티 인스펙터 창에서 힘들게 직접 바꿔가며 하지 말고 P눌러 1렙 올리고 M눌러 1렙 내리셈 ㅇㅇ 단순 테스트용임 ㅇㅇ
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GameDataManager.Instance.AxeLevel < 5)
            {
                GameDataManager.Instance.AxeLevel += 1;
                Debug.Log("도끼렙" + GameDataManager.Instance.AxeLevel);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (GameDataManager.Instance.AxeLevel > 1)
            {
                GameDataManager.Instance.AxeLevel -= 1;
                Debug.Log("도끼렙" + GameDataManager.Instance.AxeLevel);
            }
        }
    }

    void AxesRotate(int num)
    {
        _deg += rotateSpeed * Time.deltaTime;

        if (_deg < 360)
        {
            for (int i = 0; i < num; i++)
            {
                if (!axes[i].activeSelf)
                {
                    axes[i].SetActive(true);
                }

                var rotationSize = 360 / num;
                var rad = Mathf.Deg2Rad * (_deg + (i * rotationSize));
                var x = radius * Mathf.Sin(rad);
                var y = radius * Mathf.Cos(rad);

                axes[i].transform.position = target.transform.position + new Vector3(x, 0, y);
                axes[i].transform.rotation = Quaternion.Euler(90, 0, (_deg + (i * rotationSize)) * -1);
            }
        }
        else
        {
            _deg = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GameDataManager.Instance.AxeDamage);
        }
    }
}
