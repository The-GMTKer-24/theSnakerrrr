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
            Time.timeScale = paused ? 0 : 1;
            menu.gameObject.SetActive(paused);
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = paused ? 0 : 1;
        menu.gameObject.SetActive(paused);
    }
}
