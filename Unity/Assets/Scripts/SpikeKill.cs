using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeKill : MonoBehaviour
{
    [SerializeField] private String playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && PlayerManager.Instance.player.GetComponent<Rigidbody2D>().velocityY <= 0)
        {
            PlayerManager.Instance.Die();
        }
    }
}
