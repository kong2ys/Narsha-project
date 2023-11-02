using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class SelectSkill : MonoBehaviour
{
    public Image[] selectButton;

    public int total = 0;

    public List<int> skillIndex = new List<int>();
    
    public List<Skill> skills = new List<Skill>();
    public List<string> randomSkill = new List<string>();

    public int skillValue;

    void OnEnable()
    {
        Time.timeScale = 0;

        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].skillLevel >= 5)
            {
                Debug.Log(skills[i]);
                skills[i].weight = 0;
            }
            else
            {
                skills[i].weight = 100;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < skills.Count; j++)
            {
                total += skills[j].weight;
            }
            
            int weight = 0;
            int selectNum = Random.Range(0, total);
            
            for (int j = 0; j < skills.Count; j++)
            {
                weight += skills[j].weight;
                if (selectNum < weight)
                {
                    selectButton[i].sprite = skills[j].skillImage;
                    skills[j].weight = 0;
                    skillIndex.Add(j);
                    total = 0;
                    break;
                }
            }
        }
    }

    public void ButtonLeft()
    {
        skills[skillIndex[0]].skillLevel++;
        Debug.Log(skills[skillIndex[0]].skillName);
        skillValue = skills[skillIndex[0]].value;
        SkillLevelUp(skillValue);
        Finish();
    }

    public void ButtonMiddle()
    {
        skills[skillIndex[1]].skillLevel++;
        Debug.Log(skills[skillIndex[1]].skillName);
        skillValue = skills[skillIndex[1]].value;
        SkillLevelUp(skillValue);
        Finish();
    }

    public void ButtonRight()
    {
        skills[skillIndex[2]].skillLevel++;
        Debug.Log(skills[skillIndex[2]].skillName);
        skillValue = skills[skillIndex[2]].value;
        SkillLevelUp(skillValue);
        Finish();
    }

    void SkillLevelUp(int value)
    {
        switch (value)
        {
            case 0:
            {
                if (GameDataManager.Instance.FireLevel < 5)
                {
                    skills[0].skillObject.SetActive(true);
                    GameDataManager.Instance.FireLevel++;
                    Debug.Log("발사렙"+GameDataManager.Instance.FireLevel);
                }
                break;
            }
            case 1:
            {
                if (GameDataManager.Instance.AxeLevel < 5)
                {
                    skills[1].skillObject.SetActive(true);
                    GameDataManager.Instance.AxeLevel++;
                    Debug.Log("도끼렙"+GameDataManager.Instance.AxeLevel);
                }
                break;
            }
            case 2:
            {
                if (GameDataManager.Instance.DroneLevel < 5)
                {
                    skills[2].skillObject.SetActive(true);
                    GameDataManager.Instance.DroneLevel++;
                    Debug.Log("드론렙"+GameDataManager.Instance.DroneLevel);
                }
                break;
            }
            case 3:
            {
                if (GameDataManager.Instance.ArrowLevel < 5)
                {
                    skills[3].skillObject.SetActive(true);
                    GameDataManager.Instance.ArrowLevel++;
                    Debug.Log("활렙"+GameDataManager.Instance.ArrowLevel);
                }

                break;
            }
        }
    }

    public void Finish()
    {
        total = 0;
        skillIndex.Clear();
        randomSkill.Clear();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}