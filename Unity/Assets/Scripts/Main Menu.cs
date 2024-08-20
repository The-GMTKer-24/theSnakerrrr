using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(String level)
    {
        if (PlayerManager.Instance)
        {
            Destroy(PlayerManager.Instance);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    
    public void ExitGame()
    {
        if (PlayerManager.Instance)
        {
            Destroy(PlayerManager.Instance);
        }
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
