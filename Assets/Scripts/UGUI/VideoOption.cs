using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    private FullScreenMode screenMode;
    public Toggle fullscreenBtn;
    public int resolutionNum;
    public Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();
    void Start()
    {
        InitUI();
    }
    
    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144)
                resolutions.Add(Screen.resolutions[i]);
        }
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 165)
                resolutions.Add(Screen.resolutions[i]);
        }
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 240)
                resolutions.Add(Screen.resolutions[i]);
        }
        
        //resolutions.AddRange(Screen.resolutions); # 본인 프레임맞게 버튼리스트에 넣어줌
        resolutionDropdown.options.Clear();
        
        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("StartScene");
        Debug.Log("Back");
    }
}
