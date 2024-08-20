using System;
using TMPro;
using UnityEngine;

public class ScaleCounter : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    public void Update()
    {
        if (PlayerManager.Instance)
            text.SetText($"Scales: {PlayerManager.Instance.level1Collectables}");
    }
}