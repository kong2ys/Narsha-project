using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public Sprite skillImage;
    public int skillLevel;
    public int weight;
    public int value;
    public GameObject skillObject;

    public Skill(Skill skill)
    {
        this.skillName = skill.skillName;
        this.skillImage = skill.skillImage;
        this.skillLevel = skill.skillLevel;
        this.weight = skill.weight;
        this.value = skill.value;
        this.skillObject = skill.skillObject;
    }
}
