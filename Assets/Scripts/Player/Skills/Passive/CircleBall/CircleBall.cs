using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class CircleBall : MonoBehaviour
{
    public enum BallType { Fire, Ice, Poison }
    public BallType type;
    
    public Transform target;
    public GameObject[] fireBall;
    public GameObject[] iceBall;
    public GameObject[] poisonBall;

    private float[] _radius = {2, 5, 8};
    private float[]  _rotateSpeed = {150, 100, 80};

    private float _deg;

    private void Update()
    {
        switch (type)
        {
            case BallType.Fire:
            {
                switch (GameDataManager.Instance.FireBallLevel)
                {
                    case 1:
                    {
                        CircleBallRotate(1, 0);
                        break;
                    }
                    case 2:
                    {
                        CircleBallRotate(2, 0);
                        break;
                    }
                    case 3:
                    {
                        CircleBallRotate(3, 0);
                        break;
                    }
                    case 4:
                    {
                        CircleBallRotate(4, 0);
                        break;
                    }
                    case 5:
                    {
                        CircleBallRotate(5, 0);
                        break;
                    }
                }

                break;
            }
            case BallType.Ice:
            {
                switch (GameDataManager.Instance.IceBallLevel)
                {
                    case 1:
                    {
                        CircleBallRotate(1, 1);
                        break;
                    }
                    case 2:
                    {
                        CircleBallRotate(2, 1);
                        break;
                    }
                    case 3:
                    {
                        CircleBallRotate(3, 1);
                        break;
                    }
                    case 4:
                    {
                        CircleBallRotate(4, 1);
                        break;
                    }
                    case 5:
                    {
                        CircleBallRotate(5, 1);
                        break;
                    }
                }

                break;
            }
            case BallType.Poison:
            {
                switch (GameDataManager.Instance.PoisonBallLevel)
                {
                    case 1:
                    {
                        Debug.Log(231);
                        CircleBallRotate(1, 2);
                        break;
                    }
                    case 2:
                    {
                        CircleBallRotate(2, 2);
                        break;
                    }
                    case 3:
                    {
                        CircleBallRotate(3, 2);
                        break;
                    }
                    case 4:
                    {
                        CircleBallRotate(4, 2);
                        break;
                    }
                    case 5:
                    {
                        CircleBallRotate(5, 2);
                        break;
                    }
                }

                break;
            }
        }
        
    }

    void CircleBallRotate(int num, int index)
    {
        _deg += _rotateSpeed[index] * Time.deltaTime;
        
        switch (type)
        {
            case BallType.Fire:
            {
                if (_deg < 360)
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (!fireBall[i].activeSelf)
                        {
                            fireBall[i].SetActive(true);
                        }

                        var rotationSize = 360 / num;
                        var rad = Mathf.Deg2Rad * (_deg + (i * rotationSize));
                        var x = _radius[index] * Mathf.Sin(rad);
                        var z = _radius[index] * Mathf.Cos(rad);

                        fireBall[i].transform.position = target.transform.position + new Vector3(x, 0, z);
                        fireBall[i].transform.rotation = Quaternion.Euler(90, 0, (_deg + (i * rotationSize)) * -1);
                    }
                }
                else
                {
                    _deg = 0;
                }
                break;
            }
            case BallType.Ice:
            {
                if (_deg < 360)
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (!iceBall[i].activeSelf)
                        {
                            iceBall[i].SetActive(true);
                        }

                        var rotationSize = 360 / num;
                        var rad = Mathf.Deg2Rad * (_deg + (i * rotationSize));
                        var x = _radius[index] * Mathf.Sin(rad) * -1;
                        var z = _radius[index] * Mathf.Cos(rad) * -1;

                        iceBall[i].transform.position = target.transform.position + new Vector3(x, 0, -z);
                        iceBall[i].transform.rotation = Quaternion.Euler(90, 0, -(_deg + (i * rotationSize)) * -1);
                    }
                }
                else
                {
                    _deg = 0;
                }
                break;
            }
            case BallType.Poison:
            {
                if (_deg < 360)
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (!poisonBall[i].activeSelf)
                        {
                            poisonBall[i].SetActive(true);
                        }

                        var rotationSize = 360 / num;
                        var rad = Mathf.Deg2Rad * (_deg + (i * rotationSize));
                        var x = _radius[index] * Mathf.Sin(rad);
                        var z = _radius[index] * Mathf.Cos(rad);

                        poisonBall[i].transform.position = target.transform.position + new Vector3(x, 0, z);
                        poisonBall[i].transform.rotation = Quaternion.Euler(90, 0, (_deg + (i * rotationSize)) * -1);
                    }
                }
                else
                {
                    _deg = 0;
                }
                break;
            }
        }

    }
    
    void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case BallType.Fire:
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(GameDataManager.Instance.FireBallDamage);
                }
                break;
            }
            case BallType.Ice:
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(GameDataManager.Instance.IceBallDamage);
                }
                break;
            }
            case BallType.Poison:
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(GameDataManager.Instance.PoisonBallDamage);
                }
                break;
            }
        }
    }
}
