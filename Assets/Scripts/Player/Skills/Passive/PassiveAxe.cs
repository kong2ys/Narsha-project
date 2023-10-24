using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PassiveAxe : MonoBehaviour
{
    //private GameDataManager _gameData = new GameDataManager();
    
    public Transform target;
    public GameObject[] axes;
    
    public float radius;
    public float rotateSpeed;

    private float _deg;

    private void Update()
    {
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
}
