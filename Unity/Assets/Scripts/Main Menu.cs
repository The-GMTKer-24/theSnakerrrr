using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        // Load saved volume
        VolumeController.volume = PlayerPrefs.GetFloat("Volume", 1);
        AudioListener.volume = VolumeController.volume;
    }
    
    public void LoadLevel(String level)
    {
        if (PlayerManager.Instance)
        {
            Destroy(PlayerManager.Instance.gameObject);
        }
        
        // Save volume
        PlayerPrefs.SetFloat("Volume", VolumeController.volume);
        PlayerPrefs.Save();
        
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    
    public void ExitGame()
    {
        if (PlayerManager.Instance)
        {
            Destroy(PlayerManager.Instance);
        }
        
        // Save volume
        PlayerPrefs.SetFloat("Volume", VolumeController.volume);
        PlayerPrefs.Save();
        
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
