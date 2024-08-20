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

    public void Update()
    {
        time += Time.deltaTime;
        text.SetText($"{time}");
    }
}