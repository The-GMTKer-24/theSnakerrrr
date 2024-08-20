using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    private float time;
    
    public void Awake()
    {
        time = 0;
    }
    
    public void InitializeTime(float newTime)
    {
        time = newTime;
    }

    public float GetTime()
    {
        return time;
    }

    public void Update()
    {
        time += Time.deltaTime;
        TimeSpan formattedTime = TimeSpan.FromSeconds(time);
        
        text.SetText($"{formattedTime:mm\\:ss\\.fff}");
    }
}