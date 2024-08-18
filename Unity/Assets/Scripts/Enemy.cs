using System;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject deathParticles;
    [SerializeField] protected float deathParticlesLength;
    [SerializeField] protected string playerTag = "Player";
    public void Die()
    {
        Destroy(Instantiate(deathParticles,transform.position,quaternion.identity),deathParticlesLength);
        Destroy(gameObject);
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerManager.Instance.Die();
        }
    }
}