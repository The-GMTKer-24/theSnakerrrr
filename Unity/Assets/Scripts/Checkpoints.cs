using System;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private string playerTag;
    public static Checkpoints LastCheckpoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            LastCheckpoint = this;
        }
    }
}