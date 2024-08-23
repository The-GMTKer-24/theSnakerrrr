using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMaker : MonoBehaviour
{
    private bool paused;
    [SerializeField]
    private GameObject menu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            SetupPause();
        }
    }

    public void Resume()
    {
        paused = false;
        SetupPause();
    }

    public void SetupPause()
    {
        if (PlayerManager.Instance)
        {
            if (PlayerManager.Instance.playershoot)
            {
                PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().temporarilyDisabled = paused;
            }            
        }
        Time.timeScale = paused ? 0 : 1;
        menu.gameObject.SetActive(paused);
    }
}
