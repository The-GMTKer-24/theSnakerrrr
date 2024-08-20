using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private TMP_Text time;

    [SerializeField] private TMP_Text deaths;
    // Start is called before the first frame update
    void Start()
    {
        time.SetText($"{TimeSpan.FromSeconds(PlayerManager.Instance.speedrunTime):h\\:mm\\:ss\\.fff}");
        deaths.SetText($"{TimeSpan.FromSeconds(PlayerManager.Instance.deathCount):h\\:mm\\:ss\\.fff}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
