using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKill : MonoBehaviour
{
    [SerializeField] private String playerTag = "Player";
    [SerializeField] private bool delayKill;
    [SerializeField] private float timeToDelay;
    [SerializeField] private float gravityIncrease = 4;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(playerTag))
        {
            KillPlayer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            KillPlayer();
        }
        
    }
    private void KillPlayer()
    {
        if (!delayKill)
            PlayerManager.Instance.Die();
        else
            PlayerManager.Instance.DelayPlayerKill(timeToDelay,gravityIncrease);
    }

}
