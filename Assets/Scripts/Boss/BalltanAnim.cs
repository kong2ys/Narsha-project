using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalltanAnim : MonoBehaviour
{
    public Animator anim;

    Balltan _boss;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _boss = GetComponent<Balltan>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Move",_boss.pattern == 0);
        anim.SetBool("Scream", _boss.pattern == 3);
    }
}
