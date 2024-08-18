using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKill : MonoBehaviour
{
    [SerializeField] private String playerTag = "Player";
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(playerTag))
        {
            PlayerManager.Instance.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerManager.Instance.Die();
        }
        
    }

}
