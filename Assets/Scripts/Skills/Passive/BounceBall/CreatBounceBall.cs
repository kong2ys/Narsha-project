using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatBounceBall : MonoBehaviour
{
    public GameObject bounceBallFactory;

    private void OnEnable()
    {
        Instantiate(bounceBallFactory);
        gameObject.SetActive(false);
    }
}
