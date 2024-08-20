using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private TMP_Text time;

    [SerializeField] private TMP_Text deaths;

    [SerializeField] private TMP_Text scales;
    // Start is called before the first frame update
    void Start()
    {
        time.SetText($"{TimeSpan.FromSeconds(StatPasser.Instance.time):h\\:mm\\:ss\\.f}");
        deaths.SetText($"Deaths: {StatPasser.Instance.deaths}");
        scales.SetText($"Scales: {StatPasser.Instance.scales}/12");
        Destroy(StatPasser.Instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
