using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        SceneManager.LoadScene("Scenes/PlayerScene");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");
        SceneManager.LoadScene("Scenes/Resolution");
    }

    public void OnClickQuit()
    {
        Debug.Log("종료");
        //Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
    
}
