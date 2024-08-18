using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject deathParticles;
    [SerializeField] protected float deathParticlesLength;
    [SerializeField] protected string playerTag = "Player";
    private static List<Enemy> deadEnemies;

    public void Awake()
    {
        deadEnemies = new List<Enemy>();
    }
    
    public void Die()
    {
        Destroy(Instantiate(deathParticles,transform.position,quaternion.identity),deathParticlesLength);
        this.gameObject.SetActive(false);
        deadEnemies.Add(this);
    }

    public void OnDestroy()
    {
        deadEnemies.Remove(this);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerManager.Instance.Die();
        }
    }
}